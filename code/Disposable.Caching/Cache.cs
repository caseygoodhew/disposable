using System;
using System.Linq;
using System.Runtime.Caching;

namespace Disposable.Caching
{
    public class Cache : ICache
    {
        // TODO: Make this thread safe
        
        private readonly MemoryCache providerCache;
        
        private readonly MemoryCache itemCache;
        
        public Cache()
        {
            providerCache = new MemoryCache(string.Empty);
            itemCache = new MemoryCache(string.Empty);
        }

        public void Register<T>(Func<T> providerFunc)
        {
            var name = GetName<T>();

            if (providerCache.Contains(name))
            {
                throw new AlreadyRegisteredException();
            }
            
            providerCache.Add(name, providerFunc, new CacheItemPolicy());
        }

        public T Get<T>()
        {
            var name = GetName<T>();

            if (itemCache.Contains(name))
            {
                return (T)itemCache.Get(name);
            }

            if (providerCache.Contains(name))
            {
                var provider = (Func<T>)providerCache.Get(name);
                var item = provider.Invoke();
                itemCache.Add(name, item, new CacheItemPolicy());
                return item;
            }

            throw new NotRegisteredException();
        }

        public void Reload<T>()
        {
            var name = GetName<T>();

            if (itemCache.Contains(name))
            {
                itemCache.Remove(name);
            }
        }

        public void Clear()
        {
            itemCache.Select(x => x.Key).ToList().ForEach(x => itemCache.Remove(x));
        }

        private static string GetName<T>()
        {
            return typeof(T).FullName;
        }
    }
}
