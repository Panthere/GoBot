using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Exceptions;
using PokemonGo.RocketAPI.Extensions;
using PokemonGo.RocketAPI.GeneratedCode;
using PokemonGo.RocketAPI.Helpers;
using GoBot.Logic;

namespace GoBot.Utils
{
    public class Statistics
    {
        public static int _totalExperience;
        public static int _totalPokemons;
        public static int _totalItemsRemoved;
        public static int _totalPokemonsTransfered;
        public static int _totalStardust;
        public static string _currentLevelInfos;
        public static int Currentlevel = -1;

        public static DateTime _initSessionDateTime = DateTime.Now;

        public static double _getSessionRuntime()
        {
            return ((DateTime.Now - _initSessionDateTime).TotalSeconds) / 3600;
        }

        public void addExperience(int xp)
        {
            _totalExperience += xp;
        }

        public async Task<string> _getcurrentLevelInfos(Inventory _inventory)
        {
            var stats = await _inventory.GetPlayerStats();
            var output = string.Empty;
            PlayerStats stat = stats.FirstOrDefault();
            if (stat != null)
            {
                var _ep = (stat.NextLevelXp - stat.PrevLevelXp) - (stat.Experience - stat.PrevLevelXp);
                var _hours = Math.Round(_ep / (_totalExperience / _getSessionRuntime()), 2);

                output = $"Currently {stat.Level} - Level Up in {_hours} hour(s) (Required XP: {_ep})";
            }
            return output;
        }

        public void increasePokemons()
        {
            _totalPokemons += 1;
        }

        public void getStardust(int stardust)
        {
            _totalStardust = stardust;
        }

        public void addItemsRemoved(int count)
        {
            _totalItemsRemoved += count;
        }

        public void increasePokemonsTransfered()
        {
            _totalPokemonsTransfered += 1;
        }

        public async void updateConsoleTitle(Inventory _inventory)
        {
            _currentLevelInfos = await _getcurrentLevelInfos(_inventory);
            //Console.Title = ToString();
        }

        public static string ExperiencePerHour
        {
            get
            {
                return string.Format("XP/H:  {0:0.0}", _totalExperience / _getSessionRuntime());
            }
        }
        public static string PokemonFound
        {
            get
            {
                return "Pokemon Found: " + _totalPokemons.ToString();
            }
        }
        public static string Stardust
        {
            get
            {
                return "Stardust: " + _totalStardust.ToString();
            }

            
        }
        public static string PokemonTransferred
        {
            get
            {
                return "Pokemon Transferred: " + _totalPokemonsTransfered.ToString();
            }

        }

        public override string ToString()
        {
            return string.Format("LvL: {1:0}{0}EXP/H: {2:0.0} EXP{0}P/H: {3:0.0} Pokemon(s){0}Stardust: {4:0}{0}Pokemon Transfered: {5:0}{0}Items Removed: {6:0}{0}", Environment.NewLine, _currentLevelInfos, _totalExperience / _getSessionRuntime(), _totalPokemons / _getSessionRuntime(), _totalStardust, _totalPokemonsTransfered, _totalItemsRemoved);
        }
    }
}
