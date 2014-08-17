using System;

namespace Disposable.Caching
{
    public interface ICache
    {
        void Register<T>(Func<T> providerFunc);

        T Get<T>();

        void Reload<T>();

        void Clear();
    }
}
