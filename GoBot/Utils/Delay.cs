using System.Threading.Tasks;

namespace GoBot.Utils
{
    public static class T
    {
        public static async Task Delay(int delay)
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
