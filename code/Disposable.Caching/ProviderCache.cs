using System;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;

namespace Disposable.Caching
{
    /// <summary>
    /// Provides thread safe lazy loaded caching with implicit expiration.
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
        /// <param name="replaceExisting">Flag to indicate that an existing provider should be replaced.</param>
        public void Register<T>(Func<T> providerFunc, bool replaceExisting = false) where T : class
        {
            if (providerFunc == null)
            {
                throw new ArgumentNullException("providerFunc");
            }
            
            // full lock 
            cacheLock.EnterWriteLock();
            
            try
            {
                if (replaceExisting)
                {
                    UnsafeDeregister<T>();
                }
                
                UnsafeRegister(providerFunc);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Gets a cached item. If the optional <see cref="providerFunc"/> is also passed, the system should get the register it if it is not already present.
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        /// <param name="providerFunc">(Optional) A function which takes no parameters and returns an item to be stored in the cache.</param>
        /// <param name="replaceExisting">
        /// (Optional) 
        /// If <see cref="providerFunc"/> IS NOT NULL, this flag indicate that an existing provider should be replaced. 
        /// If <see cref="providerFunc"/> IS NULL, this flag indicates that than an existing item should be expired and refetched.
        /// </param>
        /// <returns>The cached item.</returns>
        public T Get<T>(Func<T> providerFunc = null, bool replaceExisting = false) where T : class
        {
            // upgradable read lock
            cacheLock.EnterUpgradeableReadLock();

            try
            {
                if (replaceExisting)
                {
                    UnsafeExpire<T>();

                    if (providerFunc != null)
                    {
                        UnsafeDeregister<T>();
                    }
                }
                
                T cachedItem;

                if (UnsafeTryGet(out cachedItem))
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
        /// Tries to get an item from the cache if it exists.
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        /// <param name="item">The cached item if it is present or null.</param>
        /// <returns>Indicates if the <see cref="item"/> was found in the cache.</returns>
        public bool TryGet<T>(out T item) where T : class
        {
            cacheLock.EnterReadLock();

            try
            {
                return UnsafeTryGet(out item);
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Expires a single item in the cache. The item will be reloaded via the registered provider on the next call to <see cref="Get{T}"/>
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        public void Expire<T>() where T : class
        {
            cacheLock.EnterWriteLock();
            
            try
            {
                UnsafeExpire<T>();
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

        private void UnsafeDeregister<T>() where T : class
        {
            providerCache.Remove(GetName<T>());
        }

        private void UnsafeExpire<T>() where T : class
        {
            itemCache.Remove(GetName<T>());
        }

        private bool UnsafeTryGet<T>(out T item) where T : class
        {
            var name = GetName<T>();

            if (itemCache.Contains(name))
            {
                item = itemCache.Get(GetName<T>()) as T;
                return true;
            }

            item = null;
            return false;
        }

        private T UnsafeFetchAndCache<T>() where T : class
        {
            var name = GetName<T>();

            var cachedProvider = providerCache.Get(name) as Func<T>;

            return UnsafeFetchAndCache(cachedProvider);
        }

        private T UnsafeFetchAndCache<T>(Func<T> cachedProvider) where T : class
        {
            var name = GetName<T>();
            
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
