using System;

namespace Disposable.Common.Conversion
{
    internal class FunctionalConverter<TFrom, TTo>  : IConvert<TFrom, TTo> where TFrom : class  where TTo : class
    {
        private readonly Func<TFrom, TTo> func;

        internal FunctionalConverter(Func<TFrom, TTo> func)
        {
            this.func = func;
        }

        public TTo Convert(TFrom @from)
        {
            return func(from);
        }
    }
}
