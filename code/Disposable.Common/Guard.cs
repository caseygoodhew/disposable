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
        public static void ArgumentIsType<T>(object value, string argumentName)
        {
            ArgumentNotNull(value, argumentName);
            
            if (!(value is T))
            {
                throw new ArgumentException(string.Format("Argument is not assignable from type {0}.", typeof(T).FullName), argumentName);
            }
        }

        public static void ArgumentIsGreaterThan(int value, int minValue, string argumentName)
        {
            if (value <= minValue)
            {
                throw new ArgumentException(string.Format("Argument {0} is less than or equal to minimum value {1}.", value, minValue), argumentName);
            }
        }

        public static void ArgumentIsGreaterThanOrEqualTo(int value, int minValue, string argumentName)
        {
            if (value < minValue)
            {
                throw new ArgumentException(string.Format("Argument {0} is less than minimum value {1}.", value, minValue), argumentName);
            }
        }
    }
}
