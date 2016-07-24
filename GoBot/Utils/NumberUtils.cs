using GoBot.UserLogger;
using PokemonGo.RocketAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoBot.Utils
{
    public static class NumberUtils
    {
        public static double ToDouble(this string str)
        {
            double o = 0;

            if (double.TryParse(str, out o))
            {
                return o;
            }
            Logger.Write($"Could not parse double: {str})");
            return o;
        }
        public static int ToInt(this string str)
        {
            int o = 0;

            if (int.TryParse(str, out o))
            {
                return o;
            }
            Logger.Write($"Could not parse int: {str})");
            return o;
        }
    }
}
