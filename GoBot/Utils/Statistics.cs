using GoBot.Logic;
using GoBot.UserLogger;
using POGOProtos.Data.Player;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        private static string _lvlUp;
        private static string _curLevel;
        private static string _reqXP;

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
            // pokeballs


            var stats = await _inventory.GetPlayerStats();
            var output = string.Empty;
            PlayerStats stat = stats.FirstOrDefault();
            if (stat != null)
            {
                
                var _ep = (stat.NextLevelXp - stat.PrevLevelXp) - (stat.Experience - stat.PrevLevelXp);
                var _hours = Math.Round(_ep / (_totalExperience / _getSessionRuntime()), 2);

                _lvlUp = _hours.ToString("0.00");
                _curLevel = stat.Level.ToString();
                _reqXP = $"{ stat.Experience - stat.PrevLevelXp - GetXpDiff(stat.Level)}/{ stat.NextLevelXp - stat.PrevLevelXp - GetXpDiff(stat.Level)}";

                output = $"{stat.Level} (LvLUp in {_hours}hours // {stat.Experience - stat.PrevLevelXp - GetXpDiff(stat.Level)}/{stat.NextLevelXp - stat.PrevLevelXp - GetXpDiff(stat.Level)} XP)";
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
            // can throw invalid response exception
            try
            {
                _currentLevelInfos = await _getcurrentLevelInfos(_inventory);
            }
            catch (Exception ex)
            {
                Logger.Write("Stat Update Exception: " + ex.ToString());
            }
            //Console.Title = ToString();
        }
        public static string ProgramRuntime
        {
            get
            {
                TimeSpan time = (DateTime.Now - _initSessionDateTime);
                return string.Format("Runtime: {0}", time.ToPretty());
                //return string.Format((DateTime.Now - _initSessionDateTime).ToString("h'hours 'm'm 's's'")
            }
        }
        public static string PlayerLevel
        {
            get
            {
                return string.Format("Level: Currently {0}", _curLevel);
            }
        }
        public static string LevelUp
        {
            get
            {
                if (_lvlUp == null)
                    return "Level up in -";
                double lvlUpDbl = _lvlUp.ToDouble();
                
                if (lvlUpDbl > 50000 || double.IsInfinity(lvlUpDbl) || double.IsNaN(lvlUpDbl) || double.IsNegativeInfinity(lvlUpDbl) || double.IsPositiveInfinity(lvlUpDbl) || lvlUpDbl == 0 || lvlUpDbl > int.MaxValue)
                    return "Level up in 50k+ hours! :(";
                return string.Format("Level up in {0}", TimeSpan.FromHours(lvlUpDbl).ToPretty());
            }
        }
        public static string ExperiencePerHour
        {
            get
            {
                return string.Format("XP/H:  {0:0.0}", _totalExperience / _getSessionRuntime());
            }
        }
        public static string RequiredXP
        {
            get
            {
                return string.Format("( Required XP:  {0:0.0} )", _reqXP);
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
        public static string PokemonPerHour
        {
            get
            {
                return string.Format("Pokemon/H:  {0:0.0}", _totalPokemons / _getSessionRuntime());
            }

        }
        public override string ToString()
        {
            return string.Format("LvL: {1:0}{0}EXP/H: {2:0.0} EXP{0}P/H: {3:0.0} Pokemon(s){0}Stardust: {4:0}{0}Pokemon Transfered: {5:0}{0}Items Removed: {6:0}{0}", Environment.NewLine, _currentLevelInfos, _totalExperience / _getSessionRuntime(), _totalPokemons / _getSessionRuntime(), _totalStardust, _totalPokemonsTransfered, _totalItemsRemoved);
        }

        public static int GetXpDiff(int level)
        {
            switch (level)
            {
                case 1:
                    return 0;
                case 2:
                    return 1000;
                case 3:
                    return 2000;
                case 4:
                    return 3000;
                case 5:
                    return 4000;
                case 6:
                    return 5000;
                case 7:
                    return 6000;
                case 8:
                    return 7000;
                case 9:
                    return 8000;
                case 10:
                    return 9000;
                case 11:
                    return 10000;
                case 12:
                    return 10000;
                case 13:
                    return 10000;
                case 14:
                    return 10000;
                case 15:
                    return 15000;
                case 16:
                    return 20000;
                case 17:
                    return 20000;
                case 18:
                    return 20000;
                case 19:
                    return 25000;
                case 20:
                    return 25000;
                case 21:
                    return 50000;
                case 22:
                    return 75000;
                case 23:
                    return 100000;
                case 24:
                    return 125000;
                case 25:
                    return 150000;
                case 26:
                    return 190000;
                case 27:
                    return 200000;
                case 28:
                    return 250000;
                case 29:
                    return 300000;
                case 30:
                    return 350000;
                case 31:
                    return 500000;
                case 32:
                    return 500000;
                case 33:
                    return 750000;
                case 34:
                    return 1000000;
                case 35:
                    return 1250000;
                case 36:
                    return 1500000;
                case 37:
                    return 2000000;
                case 38:
                    return 2500000;
                case 39:
                    return 1000000;
                case 40:
                    return 1000000;
            }
            return 0;
        }
    }
}
