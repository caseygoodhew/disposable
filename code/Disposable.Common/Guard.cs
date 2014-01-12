using System;
using System.Diagnostics.CodeAnalysis;

namespace Disposable.Common
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Guard
    {
        public static void ArgumentNotNull(object value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ArgumentNotNullOrEmpty(string value, string argumentName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
