using PokemonGo.RocketAPI;
using PokemonGo.RocketAPI.Enums;

namespace GoBot
{
    public class Settings : ISettings
    {
        public AuthType AuthType => UserSettings.Auth;
        public string PtcUsername => UserSettings.Username;
        public string PtcPassword => UserSettings.Password;
        public double DefaultLatitude => UserSettings.StartLat;
        public double DefaultLongitude => UserSettings.StartLng;
        public double DefaultAltitude = UserSettings.Altitude;
        public double WalkingSpeedInKilometerPerHour => UserSettings.WalkingSpeed;
       


        double ISettings.DefaultAltitude
        {
            get
            {
                return UserSettings.Altitude;
            }
        }

        public string GoogleRefreshToken
        {
            get { return UserSettings.GoogleRefreshToken; }
            set
            {
                UserSettings.GoogleRefreshToken = value;
            }
        }

       
    }
}