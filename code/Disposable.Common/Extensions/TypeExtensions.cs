using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="strict">If strict is true, then this type must explicitly be IEnumerable and not a type which inherits from IEnumerable. (defaults to false).</param>
        /// <returns>true if the Type is IEnumerable`1.</returns>
        public static bool IsIEnumerable(this Type source, bool strict = false)
        {
            if (source == null || !source.IsGenericType)
            {
                return false;
            }

            if (source.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return true;
            }

            if (strict)
            {
                return false;
            }

            return source.GetInterfaces().Any(x => IsIEnumerable(x, true));
        }

        /// <summary>
        /// Determines if a Type has a default constructor.
        /// </summary>
        /// <param name="source">The <see cref="T:System.Type"/> to check.</param>
        /// <param name="flags">(Optional) Binding flags to use when checking.</param>
        /// <returns>true if the Type has an default constructor.</returns>
        public static bool HasDefaultConstructor(this Type source, BindingFlags flags = BindingFlags.Default)
        {
            /************************************************************************************************************* 
             * From http://msdn.microsoft.com/en-us/library/0h6w8akb(v=vs.110).aspx
             ************************************************************************************************************* 
             The following BindingFlags filter flags can be used to define which constructors to include in the search:
                - You must specify either BindingFlags.Instance or BindingFlags.Static in order to get a return.
                - Specify BindingFlags.Public to include public constructors in the search.
                - Specify BindingFlags.NonPublic to include non-public constructors 
                           (that is, private, internal, and protected constructors) in the search.
             ************************************************************************************************************* 
             */

            if (flags == BindingFlags.Default)
            {
                flags = BindingFlags.Public;
            }

            if ((flags & BindingFlags.Static) == 0 && (flags & BindingFlags.Instance) == 0)
            {
                flags |= BindingFlags.Instance | BindingFlags.Static;
            }
            
            return source != null && source.GetConstructor(flags , null, Type.EmptyTypes, null) != null;
        }
    }
}
