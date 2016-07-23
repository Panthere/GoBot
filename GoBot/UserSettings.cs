using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.Enums;
namespace GoBot
{
    public static class UserSettings
    {
        public static string Username;
        public static string Password;
        public static AuthType Auth;
        public static string GoogleRefreshToken;

        public static double Altitude = 50;
        public static double StartLat;
        public static double StartLng;
        public static double WalkingSpeed;

        public static bool UseBerries;

        public static bool CatchPokemon;
        public static bool GetForts;

        public static int BerryProbability;
        public static int KeepCP;
        public static int EvolveOverCP;
        public static int CatchOverCP;

        public static int KeepIV;
        public static int EvolveOverIV;
        public static int CatchOverIV;

        public static int TopX;

        public static bool Teleport;
        public static bool UseDelays = true;
    }
}
