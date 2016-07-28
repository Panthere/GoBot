using System;
using PokemonGo.RocketAPI;
using PokemonGo.RocketAPI.Enums;

namespace GoBot
{
    public class Settings : ISettings
    {
        public double WalkingSpeedInKilometerPerHour => UserSettings.WalkingSpeed;
       

        public string GoogleRefreshToken
        {
            get { return UserSettings.GoogleRefreshToken; }
            set
            {
                UserSettings.GoogleRefreshToken = value;
            }
        }

        AuthType ISettings.AuthType
        {
            get
            {
                return UserSettings.Auth;
            }

            set
            {
                UserSettings.Auth = value;
            }
        }

        double ISettings.DefaultLatitude
        {
            get
            {
                return UserSettings.StartLat;
            }

            set
            {
                UserSettings.StartLat = value;
            }
        }

        double ISettings.DefaultLongitude
        {
            get
            {
                return UserSettings.StartLng;
            }

            set
            {
                UserSettings.StartLng = value;
            }
        }

        double ISettings.DefaultAltitude
        {
            get
            {
                return UserSettings.Altitude;
            }

            set
            {
                UserSettings.Altitude = value;
            }
        }

        string ISettings.PtcPassword
        {
            get
            {
                return UserSettings.Password;
            }

            set
            {
                UserSettings.Password = value;
            }
        }

        string ISettings.PtcUsername
        {
            get
            {
                return UserSettings.Username;
            }

            set
            {
                UserSettings.Username = value;
            }
        }

        public string GoogleUsername
        {
            get
            {
                return UserSettings.Username;
            }

            set
            {
                UserSettings.Username = value;
            }
        }

        public string GooglePassword
        {
            get
            {
                return UserSettings.Password;
            }

            set
            {
                UserSettings.Password = value;
            }
        }
    }
}