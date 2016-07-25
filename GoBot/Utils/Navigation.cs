using GoBot.UserLogger;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI;
using System;
using System.Threading.Tasks;

using GoogleMapsApi;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.StaticMaps;
using GoogleMapsApi.StaticMaps.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GoBot.Utils
{
    public class Navigation
    {

        private static readonly double speedDownTo = 10 / 3.6;
        private readonly Client _client;

        public  Location FinalDestination;
        public List<List<GMap.NET.PointLatLng>> DestinationSteps = new List<List<GMap.NET.PointLatLng>>();

        private Random rand = new Random();
        public Navigation(Client client)
        {
            _client = client;
        }

        public async Task<PlayerUpdateResponse> HumanLikeWalking(Location targetLocation, double walkingSpeedInKilometersPerHour, bool slowDown = true)
        {
            double speedInMetersPerSecond = walkingSpeedInKilometersPerHour / 3.6;

            Location sourceLocation = new Location(_client.CurrentLatitude, _client.CurrentLongitude);

            Logger.Write($"Distance to target location: {LocationUtils.CalculateDistanceInMeters(sourceLocation, targetLocation):0.##} meters.", LogLevel.Info);

            double nextWaypointBearing = LocationUtils.DegreeBearing(sourceLocation, targetLocation);
            double nextWaypointDistance = speedInMetersPerSecond;
            Location waypoint = LocationUtils.CreateWaypoint(sourceLocation, nextWaypointDistance, nextWaypointBearing);

            //Initial walking
            DateTime requestSendDateTime = DateTime.Now;
            var result = await _client.UpdatePlayerLocation(waypoint.Latitude, waypoint.Longitude, rand.Next((int)UserSettings.Altitude - 10, (int)UserSettings.Altitude + 10));

            do
            {
                speedInMetersPerSecond = rand.Next((int)walkingSpeedInKilometersPerHour - 5, (int)walkingSpeedInKilometersPerHour + 5) / 3.6;
                await Task.Delay(3000);
                double millisecondsUntilGetUpdatePlayerLocationResponse = (DateTime.Now - requestSendDateTime).TotalMilliseconds;

                sourceLocation = new Location(_client.CurrentLatitude, _client.CurrentLongitude);

                if (LocationUtils.CalculateDistanceInMeters(sourceLocation, targetLocation) < 40 && slowDown)
                {
                    
                    if (speedInMetersPerSecond > speedDownTo)
                    {
                        Logger.Write("We are within 40 meters of the target. Slowing down to 10 km/h to not pass the target.", LogLevel.Info);
                        speedInMetersPerSecond = speedDownTo;
                    }
                    else
                    {
                        Logger.Write("We are within 40 meters of the target, attempting to interact.", LogLevel.Info);
                    }
                }
                else
                {
                    Logger.Write($"Distance to target location: {LocationUtils.CalculateDistanceInMeters(sourceLocation, targetLocation):0.##} meters.", LogLevel.Debug);
                }

                nextWaypointDistance = millisecondsUntilGetUpdatePlayerLocationResponse / 1000 * speedInMetersPerSecond;
                nextWaypointBearing = LocationUtils.DegreeBearing(sourceLocation, targetLocation);
                waypoint = LocationUtils.CreateWaypoint(sourceLocation, nextWaypointDistance, nextWaypointBearing);

                requestSendDateTime = DateTime.Now;
                result = await _client.UpdatePlayerLocation(waypoint.Latitude, waypoint.Longitude, rand.Next((int)UserSettings.Altitude - 10, (int)UserSettings.Altitude + 10));
            } while (LocationUtils.CalculateDistanceInMeters(sourceLocation, targetLocation) >= 30);

            return result;
        }


        public async Task<PlayerUpdateResponse> DirectionalWalking(Location target, double walkSpeed)
        {
            PlayerUpdateResponse resp = null;
            FinalDestination = target;
            DestinationSteps.Clear();

            if (!UserSettings.UseGoogleDirections)
            {
                List<GMap.NET.PointLatLng> destSteps = new List<GMap.NET.PointLatLng>();
                destSteps.Add(new GMap.NET.PointLatLng(_client.CurrentLatitude, _client.CurrentLongitude));
                destSteps.Add(new GMap.NET.PointLatLng(target.Latitude, target.Longitude));
                DestinationSteps.Add(destSteps);

                return await HumanLikeWalking(target, walkSpeed, true);
            }

            DirectionsRequest directionsRequest = new DirectionsRequest()
            {
                Origin = _client.CurrentLatitude + "," + _client.CurrentLongitude,
                Destination = target.Latitude + "," + target.Longitude,
                TravelMode = UserSettings.ModeOfTravel
            };
            DirectionsResponse directions = GoogleMaps.Directions.Query(directionsRequest);

            IEnumerable<Step> steps = directions.Routes.First().Legs.First().Steps;
            foreach (var s in steps)
            {
                List<GMap.NET.PointLatLng> destSteps = new List<GMap.NET.PointLatLng>();
                destSteps.Add(new GMap.NET.PointLatLng(s.StartLocation.Latitude, s.StartLocation.Longitude));
                destSteps.Add(new GMap.NET.PointLatLng(s.EndLocation.Latitude, s.EndLocation.Longitude));
                DestinationSteps.Add(destSteps);
            }
            foreach (var s in steps)
            {
                
                Logger.Write($"Currently on step {steps.ToList().IndexOf(s) + 1} of {steps.ToList().Count}");
                //Logger.Write($"Direction Text: {s.HtmlInstructions.StripTags()}");

                resp = await HumanLikeWalking(new Location(s.EndLocation.Latitude, s.EndLocation.Longitude), walkSpeed, false);
            }
            if (_client.CurrentLatitude != target.Latitude && _client.CurrentLongitude != target.Longitude)
            {
                // do last steps
                resp = await HumanLikeWalking(target, walkSpeed);
                Logger.Write($"Corrected location to exact coordinates: {target.Latitude}, {target.Longitude}");
            }
            Logger.Write($"Client is now at {_client.CurrentLatitude}, {_client.CurrentLongitude}, target was: {target.Latitude}, {target.Longitude}");
            return resp;
        }

        public class Location
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }

            public Location(double latitude, double longitude)
            {
                Latitude = latitude;
                Longitude = longitude;
            }
        }
    }
}