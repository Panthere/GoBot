using PokemonGo.RocketAPI.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGo.RocketAPI;

namespace GoBot.UserLogger
{
    public class EventLogger : ILogger
    {
        private LogLevel maxLogLevel;

        public EventLogger(LogLevel maxLogLevel)
        {
            this.maxLogLevel = maxLogLevel;
        }

        public void Write(string message, LogLevel level = LogLevel.Info)
        {
            if (level > maxLogLevel)
                return;
            Utils.Events.Log("Main", message);
            //throw new NotImplementedException();
        }

        public void Write(string message, LogLevel level = LogLevel.Info, ConsoleColor color = ConsoleColor.Black)
        {
            // Quick fix, this is not going to stay.

            if (level > maxLogLevel)
                return;
            Utils.Events.Log("Main", message);
            //throw new NotImplementedException();
        }
    }
}
