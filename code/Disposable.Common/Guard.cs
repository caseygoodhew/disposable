using System;

namespace Disposable.Common
{
    /// <summary>
    /// Validates arguments and raises appropriate Argument exception if the test fails
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Validates that an object is not null
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="argumentName">The name of the argument</param>
        public static void ArgumentNotNull(object value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Validates that a string is not null or empty
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="argumentName">The name of the argument</param>
        public static void ArgumentNotNullOrEmpty(string value, string argumentName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Validates that an object is assignable from the specified generic type
        /// </summary>
        /// <typeparam name="T">The generic type to validate assignment against</typeparam>
        /// <param name="value">The value to validate</param>
        /// <param name="argumentName">The name of the argument</param>
        public static void ArgumentIsType<T>(object value, string argumentName) where T : class
        {
            if (!value.GetType().IsAssignableFrom(typeof(T)))
            {
                throw new ArgumentException(argumentName);
            }
        }
    }
}
