using POGOProtos.Inventory;
using POGOProtos.Inventory.Item;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GoBot.Utils
{
    public static class StringUtils
    { 
        public static string GetSummedFriendlyNameOfItemAwardList(IEnumerable<ItemAward> items)
        {
            var enumerable = items as IList<ItemAward> ?? items.ToList();

            if (!enumerable.Any())
                return string.Empty;

            return
                enumerable.GroupBy(i => i.ItemId)
                          .Select(kvp => new { ItemName = kvp.Key.ToString(), Amount = kvp.Sum(x => x.ItemCount) })
                          .Select(y => $"{y.Amount} x {y.ItemName}")
                          .Aggregate((a, b) => $"{a}, {b}");
        }
        public static Color ToColor(this string str)
        {
            return Helpers.ColorFromHex(str);
        }
        public static string ToNormal(this string str)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsUpper(c) && builder.Length > 0) builder.Append(' ');
                builder.Append(c);
            }
            return builder.ToString();
        }
        public static string ToPretty(this TimeSpan time)
        {
            return string.Format("{0} hour{3}, {1} minute{4}, {2} second{5}", time.Hours, time.Minutes, time.Seconds,
                    time.Hours > 1 ? "s" : "",
                    time.Minutes > 1 ? "s" : "",
                    time.Seconds > 1 ? "s" : "");
        }
        public static string StripTags(this string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
