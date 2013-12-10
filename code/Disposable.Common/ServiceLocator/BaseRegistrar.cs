using System;
using System.Collections.Generic;

namespace Disposable.Common.ServiceLocator
{
    /// <summary>
    /// Implementation of the <see cref="IRegistrar"/> interface
    /// </summary>
    public class BaseRegistrar : IRegistrar
    {
        private readonly Dictionary<Type, Func<object>> _services = new Dictionary<Type, Func<object>>();
        
        /// <summary>
        /// Registers a locator function for type T
        /// </summary>
        /// <typeparam name="T">The generic type to register the locator function for</typeparam>
        /// <param name="locatorFunc">A function that returns an instance of T</param>
        /// <exception cref="ServiceAlreadyRegisteredException">Thrown when a locator function for the given generic type T is already registered</exception>
        public void Register<T>(Func<T> locatorFunc) where T : class
        {
            if (IsRegistered<T>())
            {
                throw new ServiceAlreadyRegisteredException(typeof(T));
            }

            _services[typeof(T)] = locatorFunc;
        }

        /// <summary>
        /// Checks to see if a locator function for type T is already registered
        /// </summary>
        /// <typeparam name="T">The generic type to look for</typeparam>
        /// <returns></returns>
        public bool IsRegistered<T>() where T : class
        {
            return _services.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Gets an instance of type T
        /// </summary>
        /// <typeparam name="T">The generic type to look for</typeparam>
        /// <returns></returns>
        /// <exception cref="ServiceNotFoundException">Thrown when a locator function for the given generic type T is not found</exception>
        public virtual T Instance<T>() where T : class
        {
            if (IsRegistered<T>())
            {
                return (T)_services[typeof(T)]();
            }

            throw new ServiceNotFoundException(typeof(T));
        }
    }
}
