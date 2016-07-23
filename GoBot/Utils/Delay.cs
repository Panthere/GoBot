using GoBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoBot.Utils
{
    public static class T
    {
        public static async void Delay(int delay)
        {
            if (UserSettings.UseDelays)
            {
                await Task.Delay(delay);
            }
            else
            {
                return;
            }
        }
    }
}
