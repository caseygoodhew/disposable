using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Disposable.Common.ServiceLocator;

namespace Disposable.Common.Conversion
{
    public static class ConverterFactory
    {
        public static TTo ConvertOne<TFrom, TTo>(TFrom obj)
            where TFrom : class
            where TTo : class
        {
            if (obj == null)
            {
                return null;
            }

            var converter = Locator.Current.Instance<IConvert<TFrom, TTo>>();

            return converter.Convert(obj);
        }

        public static IEnumerable<TTo> ConvertMany<TFrom, TTo>(IEnumerable<TFrom> obj)
            where TFrom : class
            where TTo : class
        {
            if (obj == null)
            {
                return null;
            }
            
            var converter = Locator.Current.Instance<IConvert<TFrom, TTo>>();

            return obj.Select(converter.Convert);
        }

        public static void Register<TFrom, TTo>(IRegistrar registrar, Func<TFrom, TTo> func)
            where TFrom : class
            where TTo : class
        {
            registrar.Register<IConvert<TFrom, TTo>>(() => new FunctionalConverter<TFrom, TTo>(func));
        }

        public static void Register<T>(IRegistrar registrar) where T : class
        {
            registrar.Register<IConvert<T, T>>(() => new ConvertToSelf<T>());
        }
    }
}
