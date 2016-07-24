using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.Enums;
using AllEnum;

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

        public static List<int> recycleSettings = new List<int>();

        public static ICollection<KeyValuePair<ItemId, int>> ItemRecycleFilter
        {
            get
            {
                return new[]
                {
                    new KeyValuePair<ItemId, int>(ItemId.ItemUnknown, 0),
                    new KeyValuePair<ItemId, int>(ItemId.ItemPokeBall, recycleSettings[0]),
                    new KeyValuePair<ItemId, int>(ItemId.ItemGreatBall, recycleSettings[1]),
                    new KeyValuePair<ItemId, int>(ItemId.ItemUltraBall, recycleSettings[2]),
                    new KeyValuePair<ItemId, int>(ItemId.ItemPotion, recycleSettings[3]),
                    new KeyValuePair<ItemId, int>(ItemId.ItemSuperPotion, recycleSettings[4]),
                    new KeyValuePair<ItemId, int>(ItemId.ItemHyperPotion, recycleSettings[5]),
                    new KeyValuePair<ItemId, int>(ItemId.ItemMaxPotion, recycleSettings[6]),
                    new KeyValuePair<ItemId, int>(ItemId.ItemRevive, recycleSettings[7]),
                    new KeyValuePair<ItemId, int>(ItemId.ItemMaxRevive, recycleSettings[8]),
                    new KeyValuePair<ItemId, int>(ItemId.ItemRazzBerry, recycleSettings[9])
                };
            }
        }
    }
}
