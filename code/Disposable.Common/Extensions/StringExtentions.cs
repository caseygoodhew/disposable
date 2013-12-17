using System.Globalization;
using System.Text.RegularExpressions;

namespace Disposable.Common.Extensions
{
    public static class StringExtentions
    {
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
