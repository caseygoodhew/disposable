using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Disposable.Common.Extensions
{
    public static class EnumExtensions
    {
        public static bool IsEnum<T>()
        {
            return typeof(T).IsEnum;
        }

        public static void Guard<T>()
        {
            if (!IsEnum<T>())
            {
                throw new InvalidEnumArgumentException(string.Format("{0} is not an enum", typeof(T).FullName));
            }
        }
        
        public static IEnumerable<T> GetValues<T>()
        {
            return All<T>();
        }

        public static IEnumerable<T> All<T>()
        {
            Guard<T>();
            return Enum.GetValues(typeof(T)).Cast<T>(); 
        }

        public static IEnumerable<T> Except<T>(params T[] values)
        {
            return All<T>().Where(x => !values.Contains(x));
        }
    }
}
