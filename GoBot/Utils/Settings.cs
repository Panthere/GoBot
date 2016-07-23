using System.Configuration;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.GeneratedCode;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using PokemonGo.RocketAPI;

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
        public List<int> recycleSettings = new List<int>();


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

        public float KeepMinIVPercentage
        {
            get
            {
                return UserSettings.KeepIV / 100;
            }
        }

        public int KeepMinCP
        {
            get
            {
                return UserSettings.KeepCP;
            }
        }

        public ICollection<KeyValuePair<ItemId, int>> ItemRecycleFilter
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

        public ICollection<PokemonId> PokemonsToEvolve
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool EvolveAllPokemonWithEnoughCandy
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool TransferDuplicatePokemon
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int DelayBetweenMove
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool UsePokemonToNotCatchFilter
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ICollection<PokemonId> PokemonsNotToTransfer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ICollection<PokemonId> PokemonsNotToCatch
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}