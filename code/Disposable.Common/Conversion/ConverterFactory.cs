using System;
using System.Collections.Generic;
using System.Linq;

using Disposable.Common.ServiceLocator;

namespace Disposable.Common.Conversion
{
    public static class ConverterFactory
    {
        public static TTo ConvertOne<TFrom, TTo>(TFrom fromType)
            where TFrom : class
            where TTo : class
        {
            return Locator.Current.Instance<IConvert<TFrom, TTo>>().Convert(fromType);
        }

        public static IEnumerable<TTo> ConvertMany<TFrom, TTo>(IEnumerable<TFrom> fromType)
            where TFrom : class
            where TTo : class
        {
            var converter = Locator.Current.Instance<IConvert<TFrom, TTo>>();

            return fromType.Select(converter.Convert);
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
