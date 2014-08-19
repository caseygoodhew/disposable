using System;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;

namespace Disposable.Caching
{
    /// <summary>
    /// Provides thread safe lazy loaded caching with implicit expriation.
    /// </summary>
    public class ProviderCache : IProviderCache
    {
        private static readonly string _cacheName = "Disposable";

        private readonly MemoryCache providerCache = new MemoryCache(_cacheName);

        private readonly MemoryCache itemCache = new MemoryCache(_cacheName);

        private readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        
        /// <summary>
        /// Registers a cache item provider.
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        /// <param name="providerFunc">A function which takes no parameters and returns an item to be stored in the cache.</param>
        public void Register<T>(Func<T> providerFunc) where T : class
        {
            if (providerFunc == null)
            {
                throw new ArgumentNullException("providerFunc");
            }
            
            // full lock 
            cacheLock.EnterWriteLock();

            try
            {
                UnsafeRegister(providerFunc);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Gets a cached item. If the optional <see cref="providerFunc"/> is also passed, the system will get the register it if it is not already present.
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        /// <param name="providerFunc">(Optional) A function which takes no parameters and returns an item to be stored in the cache.</param>
        /// <returns>The cached item.</returns>
        public T Get<T>(Func<T> providerFunc = null) where T : class
        {
            T cachedItem;
            
            // upgradable read lock
            cacheLock.EnterUpgradeableReadLock();

            try
            {
                cachedItem = UnsafeGetIfExists<T>();

                if (cachedItem != null)
                {
                    return cachedItem;
                }

                // full lock 
                cacheLock.EnterWriteLock();

                try
                {
                    if (UnsafeHasProvider<T>())
                    {
                        return UnsafeFetchAndCache<T>();
                    }
                    else if (providerFunc != null)
                    {
                        UnsafeRegister(providerFunc);
                        return UnsafeFetchAndCache<T>();
                    }
                }
                finally
                {
                    cacheLock.ExitWriteLock();
                }
            }
            finally
            {
                cacheLock.ExitUpgradeableReadLock();
            }

            throw new NotRegisteredException();
        }

        /// <summary>
        /// Expires a single item in the cache. The item will be reloaded via the registered provider on the next call to <see cref="Get{T}"/>
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        public void Expire<T>() where T : class
        {
            var name = GetName<T>();

            cacheLock.EnterWriteLock();
            
            try
            {
                itemCache.Remove(name);
            }
            finally 
            {
                cacheLock.ExitWriteLock();   
            }
        }

        /// <summary>
        /// Expires all items in the cache. Items will be reloaded via their registered providers on the following calls to <see cref="Get{T}"/>
        /// </summary>
        public void ExpireAll()
        {
            cacheLock.EnterWriteLock();

            try
            {
                itemCache.Select(x => x.Key).ToList().ForEach(x => itemCache.Remove(x));
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        private static string GetName<T>()
        {
            return typeof(T).FullName;
        }

        private bool UnsafeHasProvider<T>() where T : class
        {
            return providerCache.Contains(GetName<T>());
        }
        
        private void UnsafeRegister<T>(Func<T> providerFunc) where T : class
        {
            var name = GetName<T>();
            
            if (providerCache.Contains(name))
            {
                throw new AlreadyRegisteredException();
            }

            providerCache.Add(name, providerFunc, new CacheItemPolicy());
        }

        private T UnsafeGetIfExists<T>() where T : class
        {
            return itemCache.Get(GetName<T>()) as T;
        }

        private T UnsafeFetchAndCache<T>() where T : class
        {
            var name = GetName<T>();

            var cachedProvider = providerCache.Get(name) as Func<T>;

            if (cachedProvider == null)
            {
                return null;
            }

            var providedItem = cachedProvider.Invoke();
            itemCache.Add(name, providedItem, new CacheItemPolicy());
            return providedItem;
        }
    }
}
