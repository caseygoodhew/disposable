using System;
using System.Web;

using Disposable.Caching;

namespace Disposable.Web.Caching
{
    public class RequestCache : IProviderCache
    {
        internal const string ItemKey = "Disposable.Web.Caching.RequestCache";

        private readonly IProviderCache providerCache;

        /// <summary>
        /// Static method to get the current <see cref="RequestCache"/>. If an instance does not exist in the current <see cref="HttpContext"/>, it is created.
        /// </summary>
        public static RequestCache Current
        {
            get
            {
                RequestCache requestCache;
                
                if (HttpContext.Current.Items.Contains(ItemKey))
                {
                    var requestItem = HttpContext.Current.Items[ItemKey];

                    if (requestItem == null)
                    {
                        throw new NullReferenceException(string.Format("RequestCache key ({0}) in use but is null.", ItemKey));
                    }

                    requestCache = requestItem as RequestCache;

                    if (requestCache == null)
                    {
                        throw new InvalidOperationException(string.Format("RequestCache key ({0}) in use but is not a RequestCache instance.", ItemKey));
                    }

                    return requestCache;
                }

                requestCache = new RequestCache(new ProviderCache());

                HttpContext.Current.Items[ItemKey] = requestCache;
                
                return requestCache;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestCache"/> class.
        /// </summary>
        /// <param name="providerCache">The backing <see cref="IProviderCache"/> (normally a <see cref="ProviderCache"/> instance).</param>
        internal RequestCache(IProviderCache providerCache)
        {
            this.providerCache = providerCache;
        }

        /// <summary>
        /// Registers a cache item provider.
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        /// <param name="providerFunc">A function which takes no parameters and returns an item to be stored in the cache.</param>
        public void Register<T>(Func<T> providerFunc) where T : class
        {
            providerCache.Register(providerFunc);
        }

        /// <summary>
        /// Gets a cached item. If the optional <see cref="providerFunc"/> is also passed, the system should get the register it if it is not already present.
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        /// <param name="providerFunc">(Optional) A function which takes no parameters and returns an item to be stored in the cache.</param>
        /// <returns>The cached item.</returns>
        public T Get<T>(Func<T> providerFunc = null) where T : class
        {
            return providerCache.Get(providerFunc);
        }

        /// <summary>
        /// Expires a single item in the cache. The item should be reloaded via the registered provider on the next call to <see cref="IProviderCache.Get{T}"/>
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        public void Expire<T>() where T : class
        {
            providerCache.Expire<T>();
        }

        /// <summary>
        /// Expires all items in the cache. Items should be reloaded via their registered providers on the following calls to <see cref="IProviderCache.Get{T}"/>
        /// </summary>
        public void ExpireAll()
        {
            providerCache.ExpireAll();
        }
    }
}
