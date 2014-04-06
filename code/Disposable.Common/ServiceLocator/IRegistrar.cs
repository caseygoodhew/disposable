using System;

namespace Disposable.Common.ServiceLocator
{
    /// <summary>
    /// Service locator function registration services
    /// </summary>
    public interface IRegistrar
    {
        /// <summary>
        /// Registers a locator function for type T
        /// </summary>
        /// <typeparam name="T">The generic type to register the locator function for</typeparam>
        /// <param name="locatorFunc">A function that returns an instance of T</param>
        void Register<T>(Func<T> locatorFunc) where T : class;

        /// <summary>
        /// Checks to see if a locator function for type T is already registered
        /// </summary>
        /// <typeparam name="T">The generic type to look for</typeparam>
        /// <returns>The instance</returns>
        bool IsRegistered<T>() where T : class;

        /// <summary>
        /// Checks to see if a locator function for given <see cref="Type"/> is already registered
        /// </summary>
        /// <param name="type">The type to look for</param>
        /// <returns>The instance</returns>
        bool IsRegistered(Type type);
    }
}
