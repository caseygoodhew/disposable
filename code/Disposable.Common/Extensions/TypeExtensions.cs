using System;
using System.Collections.Generic;
using System.Reflection;

namespace Disposable.Common.Extensions
{
    /// <summary>
    /// Type extensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines if a Type is IEnumerable`1.
        /// </summary>
        /// <param name="source">The <see cref="T:System.Type"/> to check.</param>
        /// <returns>true if the Type is IEnumerable`1.</returns>
        public static bool IsIEnumerable(this Type source)
        {
            return source != null && source.IsGenericType && source.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        /// <summary>
        /// Determines if a Type has a default constructor.
        /// </summary>
        /// <param name="source">The <see cref="T:System.Type"/> to check.</param>
        /// <param name="flags">(Optional) Binding flags to use when checking.</param>
        /// <returns>true if the Type has an default constructor.</returns>
        public static bool HasDefaultConstructor(this Type source, BindingFlags flags = BindingFlags.Default)
        {
            return source != null && source.GetConstructor(flags, null, Type.EmptyTypes, null) != null;
        }
    }
}
