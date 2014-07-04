using System;
using System.Collections.Generic;
using System.Linq;

namespace Disposable.Common.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsIEnumerable(this Type source)
        {
            return source != null && source.IsGenericType && source.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        public static bool ImplementsIEnumerable(this Type source)
        {
            if (source.IsIEnumerable())
            {
                return true;
            }

            return source != null &&
                   source.IsGenericType &&
                   source.GetInterfaces()
                       .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        private static void IsTest<T>()
        {
            
        }
    }
}
