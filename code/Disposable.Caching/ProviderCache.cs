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

        private readonly ReaderWriterLockSlim providerCacheLock = new ReaderWriterLockSlim();

        private readonly ReaderWriterLockSlim itemCacheLock = new ReaderWriterLockSlim();
        
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
            
            var name = GetName<T>();

            // full lock on the provider cache
            providerCacheLock.EnterWriteLock();

            try
            {
                if (providerCache.Contains(name))
                {
                    throw new AlreadyRegisteredException();
                }

                providerCache.Add(name, providerFunc, new CacheItemPolicy());
            }
            finally
            {
                providerCacheLock.ExitWriteLock();
            }
            
        }

        /// <summary>
        /// Gets a cached item.
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        /// <returns>The cached item.</returns>
        public T Get<T>() where T : class
        {
            var name = GetName<T>();
            T cachedItem;

            // read lock on the item cache
            itemCacheLock.EnterReadLock();

            try
            {
                cachedItem = itemCache.Get(name) as T;
            }
            finally
            {
                itemCacheLock.ExitReadLock();
            }

            if (cachedItem != null)
            {
                return cachedItem;
            }
            
            // full lock on the provider cache (a read lock would allow multiple threads to invoke the provider so we must use a write lock)
            providerCacheLock.EnterWriteLock();
            try
            {
                var cachedProvider = providerCache.Get(name) as Func<T>;

                if (cachedProvider != null)
                {
                    var providedItem = cachedProvider.Invoke();

                    // full lock the item cache once we have the item to cache
                    itemCacheLock.EnterWriteLock();

                    try
                    {
                        itemCache.Add(name, providedItem, new CacheItemPolicy());
                    }
                    finally
                    {
                        itemCacheLock.ExitWriteLock();
                    }

                    return providedItem;
                }
            }
            finally
            {
                providerCacheLock.ExitWriteLock();
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

            itemCacheLock.EnterWriteLock();
            
            try
            {
                itemCache.Remove(name);
            }
            finally 
            {
                itemCacheLock.ExitWriteLock();   
            }
        }

        /// <summary>
        /// Expires all items in the cache. Items will be reloaded via their registered providers on the following calls to <see cref="Get{T}"/>
        /// </summary>
        public void ExpireAll()
        {
            itemCacheLock.EnterWriteLock();

            try
            {
                itemCache.Select(x => x.Key).ToList().ForEach(x => itemCache.Remove(x));
            }
            finally
            {
                itemCacheLock.ExitWriteLock();
            }
        }

        private static string GetName<T>()
        {
            return typeof(T).FullName;
        }
    }
}
