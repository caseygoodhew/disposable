using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
