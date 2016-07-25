
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGo.RocketAPI;
using System.Drawing;
using GoBot.Utils;

namespace GoBot.UserLogger
{
    public class EventLogger : ILogger
    {
        private LogLevel maxLogLevel;

        public EventLogger(LogLevel maxLogLevel)
        {
            this.maxLogLevel = maxLogLevel;
        }


        public void Write(string message, LogLevel level = LogLevel.Info, ConsoleColor color = ConsoleColor.Black)
        {
            if (level > maxLogLevel)
                return;

            Utils.Events.Log("Main", message, ConsoleColorToColor(color));
        }

        private Color ConsoleColorToColor(ConsoleColor c)
        {
            switch (c)
            {
                case ConsoleColor.Blue:
                    return "#E87DDF".ToColor();
                case ConsoleColor.Cyan:
                    return "#6AD5E6".ToColor();
                case ConsoleColor.DarkBlue:
                    return "#000080".ToColor();
                case ConsoleColor.DarkCyan:
                    return "#008080".ToColor();
                case ConsoleColor.DarkGray:
                    return "#808080".ToColor();
                case ConsoleColor.DarkGreen:
                    return "#A38CFF".ToColor();
                case ConsoleColor.DarkMagenta:
                    return "#800080".ToColor();
                case ConsoleColor.DarkRed:
                    return "#800000".ToColor();
                case ConsoleColor.DarkYellow:
                    return "#E66A7F".ToColor();
                case ConsoleColor.Gray:
                    return "#C0C0C0".ToColor();
                case ConsoleColor.Green:
                    return "#00FF00".ToColor();
                case ConsoleColor.Magenta:
                    return "#FF00FF".ToColor();
                case ConsoleColor.Red:
                    return "#FFFFFF".ToColor();
                case ConsoleColor.White:
                    return "#FFFFFF".ToColor();
                case ConsoleColor.Yellow:
                    return "#FFFF00".ToColor();
                default:
                    return Color.FromArgb(220, 220, 220);

            }
        }
    }
}
