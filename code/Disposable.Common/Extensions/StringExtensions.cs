using System.Globalization;

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

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
        }
    }
}
