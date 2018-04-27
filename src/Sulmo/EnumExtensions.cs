using System;
using System.Collections.Generic;
using System.Text;

namespace Sulmo
{
    public static class EnumExtensions
    {
        public static T To<T>(this string value) where T : struct
        {
            return Enum.Parse<T>(value, true);
        }

        public static string ToLowerString(this Enum value)
        {
            return value.ToString().ToLower();
        }

        public static string ToUpperString(this Enum value)
        {
            return value.ToString().ToUpper();
        }

        public static bool IsParsable<T>(this string value) where T : struct
        {
            return Enum.TryParse<T>(value, true, out _);
        }
    }
}
