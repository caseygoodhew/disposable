
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Disposable.Common.Extensions
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string to title case using the current culture info
        /// </summary>
        /// <param name="str">The string to convert</param>
        /// <returns>The converted string</returns>
        public static string ToTitleCase(this string str)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }

            var parts = str.Split(' ');
            return parts.Concat(ToFirstCharUpperRestLower, " ");
        }

        public static string TidyWhiteSpace(this string str, int max = 3)
        {
            return str.Tidy("\r\n", max).Tidy(" ", max).Tidy("\t", max).Tidy("\v", max);
        }
        
        public static string Tidy(this string str, string pattern, int max = 3)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            Guard.ArgumentIsGreaterThanOrEqualTo(max, 0, "max");

            var rePattern = "[" + pattern + "]";
            
            if (max == 0)
            {
                return new Regex(rePattern, RegexOptions.None).Replace(str, string.Empty);
            }

            return new Regex(rePattern + "{" + max + ",}", RegexOptions.None).Replace(str, pattern.Repeat(max - 1));
        }

        public static string Repeat(this string value, int times)
        {
            Guard.ArgumentIsGreaterThan(times, 0, "times");

            return Enumerable.Repeat(value, times).Concat();
        }

        public static string Default(this string value, string defaultValue)
        {
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        public static string Default(this object value, string defaultValue)
        {
            return (value ?? string.Empty).ToString().Default(defaultValue);
        }
        
        private static string ToFirstCharUpperRestLower(string str)
        {
            if (str.Length <= 1)
            {
                return str.ToUpper();
            }
            
            return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
        }
    }
}
