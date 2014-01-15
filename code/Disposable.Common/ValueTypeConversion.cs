using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disposable.Common
{
    public static class ValueTypeConversionTBD
    {
        /*private static readonly Dictionary<Type, ValueTypes> ValueTypesMap = new Dictionary<Type, ValueTypes>
        {
            { typeof(bool), ValueTypes.Bool },
            { typeof(byte), ValueTypes.Byte },
            { typeof(char), ValueTypes.Char },
            { typeof(decimal), ValueTypes.Decimal },
            { typeof(double), ValueTypes.Double },
            { typeof(float), ValueTypes.Float },
            { typeof(int), ValueTypes.Int },
            { typeof(long), ValueTypes.Long },
            { typeof(sbyte), ValueTypes.SByte },
            { typeof(short), ValueTypes.Short },
            { typeof(uint), ValueTypes.UInt },
            { typeof(ulong), ValueTypes.ULong },
            { typeof(ushort), ValueTypes.UShort }
        };

        public static bool IsValueType(object obj)
        {
            return IsValueType(obj.GetType());
        }

        public static bool IsValueType<T>()
        {
            return IsValueType(typeof (T));
        }

        public static bool IsValueType(Type type)
        {
            return ValueTypesMap.ContainsKey(type);
        }

        public static ValueTypes GetValueType(object obj)
        {
            return GetValueType(obj.GetType());
        }

        public static ValueTypes GetValueType<T>()
        {
            return GetValueType(typeof (T));
        }

        public static ValueTypes GetValueType(Type type)
        {
            return ValueTypesMap.First(x => x.Key == type).Value;
        }*/
    }
}
