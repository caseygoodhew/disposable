using System;

namespace Disposable.Caching
{
    /// <summary>
    /// Interface for classes which provide lazy loaded caching with implicit expiration.
    /// </summary>
    public interface IProviderCache
    {
        /// <summary>
        /// Registers a cache item provider.
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        /// <param name="providerFunc">A function which takes no parameters and returns an item to be stored in the cache.</param>
        /// <param name="replaceExisting">Flag to indicate that an existing provider should be replaced.</param>
        void Register<T>(Func<T> providerFunc, bool replaceExisting = false) where T : class;

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
        T Get<T>(Func<T> providerFunc = null, bool replaceExisting = false) where T : class;

        /// <summary>
        /// Tries to get an item from the cache if it exists.
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        /// <param name="item">The cached item if it is present or null.</param>
        /// <returns>Indicates if the <see cref="item"/> was found in the cache.</returns>
        bool TryGet<T>(out T item) where T : class;

        /// <summary>
        /// Expires a single item in the cache. The item should be reloaded via the registered provider on the next call to <see cref="Get{T}"/>
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        void Expire<T>() where T : class;

        /// <summary>
        /// Expires all items in the cache. Items should be reloaded via their registered providers on the following calls to <see cref="Get{T}"/>. 
        /// </summary>
        void ExpireAll();

        /// <summary>
        /// Explicitly sets a cached item. It a value is already set, it will be replaced.
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        /// <param name="item">The item to cache. Cannot be null.</param>
        void Set<T>(T item) where T : class;
    }
}
