using System;

namespace Disposable.Caching
{
    /// <summary>
    /// Interface for classes which provide lazy loaded caching with implicit expriation.
    /// </summary>
    public interface IProviderCache
    {
        /// <summary>
        /// Registers a cache item provider.
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        /// <param name="providerFunc">A function which takes no parameters and returns an item to be stored in the cache.</param>
        void Register<T>(Func<T> providerFunc) where T : class;

        /// <summary>
        /// Gets a cached item.
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        /// <returns>The cached item.</returns>
        T Get<T>() where T : class;

        /// <summary>
        /// Expires a single item in the cache. The item should be reloaded via the registered provider on the next call to <see cref="Get{T}"/>
        /// </summary>
        /// <typeparam name="T">The cache item key.</typeparam>
        void Expire<T>() where T : class;

        /// <summary>
        /// Expires all items in the cache. Items should be reloaded via their registered providers on the following calls to <see cref="Get{T}"/>
        /// </summary>
        void ExpireAll();
    }
}
