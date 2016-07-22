using System;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.GeneratedCode;
using PokemonGo.RocketAPI;

namespace GoBot.Utils
{
    public class Navigation
    {

        private static readonly double speedDownTo = 10 / 3.6;
        private readonly Client _client;

        private Random rand = new Random();
        public Navigation(Client client)
        {
            _client = client;
        }

        public async Task<PlayerUpdateResponse> HumanLikeWalking(Location targetLocation, double walkingSpeedInKilometersPerHour)
        {
            double speedInMetersPerSecond = walkingSpeedInKilometersPerHour / 3.6;

            Location sourceLocation = new Location(_client.CurrentLat, _client.CurrentLng);

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

                sourceLocation = new Location(_client.CurrentLat, _client.CurrentLng);

                if (LocationUtils.CalculateDistanceInMeters(sourceLocation, targetLocation) < 40)
                {
                    if (speedInMetersPerSecond > speedDownTo)
                    {
                        Logger.Write("We are within 40 meters of the target. Speeding down to 10 km/h to not pass the target.", LogLevel.Info);
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