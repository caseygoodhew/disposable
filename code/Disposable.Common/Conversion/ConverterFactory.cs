using System;

using Disposable.Common.ServiceLocator;

namespace Disposable.Common.Conversion
{
    public static class ConverterFactory
    {
        public static TTo Convert<TFrom, TTo>(TFrom from) where TFrom : class where TTo : class
        {
            return Locator.Current.Instance<IConvert<TFrom, TTo>>().Convert(from);
        }

        public static void Register<TFrom, TTo>(IRegistrar registrar, Func<TFrom, TTo> func)
            where TFrom : class
            where TTo : class
        {
            registrar.Register<IConvert<TFrom, TTo>>(() => new FunctionalConverter<TFrom, TTo>(func));
        }
    }
}
