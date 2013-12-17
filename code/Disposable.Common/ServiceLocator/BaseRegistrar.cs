using System;
using System.Collections.Generic;

namespace Disposable.Common.ServiceLocator
{
    /// <summary>
    /// Implementation of the <see cref="IRegistrar"/> interface
    /// </summary>
    public class BaseRegistrar : IRegistrar, ILocator
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
            return IsRegistered(typeof(T));
        }

        /// <summary>
        /// Checks to see if a locator function for given <see cref="Type"/> is already registered
        /// </summary>
        /// <param name="type">The type to look for</param>
        /// <returns></returns>
        public bool IsRegistered(Type type)
        {
            return _services.ContainsKey(type);
        }

        /// <summary>
        /// Gets an instance of type T
        /// </summary>
        /// <typeparam name="T">The generic type to look for</typeparam>
        /// <returns></returns>
        /// <exception cref="ServiceNotFoundException">Thrown when a locator function for the given generic type T is not found</exception>
        public T Instance<T>() where T : class
        {
            T instance;
            if (TryGetInstance(out instance))
            {
                return instance;
            }

            throw new ServiceNotFoundException(typeof(T));
        }

        /// <summary>
        /// Gets the implementation of a given <see cref="Type"/>
        /// </summary>
        /// <param name="type">The type to retrieve</param>
        /// <returns>The type instance</returns>
        public object Instance(Type type)
        {
            object instance;
            if (TryGetInstance(type, out instance))
            {
                return instance;
            }

            throw new ServiceNotFoundException(type);
        }

        /// <summary>
        /// Tries to get the implementation of type T
        /// </summary>
        /// <typeparam name="T">The type to try to retrieve</typeparam>
        /// <param name="instance">The type instance if found</param>
        /// <returns>true if the type is found, otherwise false</returns>
        public virtual bool TryGetInstance<T>(out T instance) where T : class
        {
            if (IsRegistered<T>())
            {
                instance = (T)_services[typeof(T)]();
                return true;
            }

            instance = null;
            return false;
        }

        /// <summary>
        /// Tries to get the implementation of a given <see cref="Type"/>
        /// </summary>
        /// <param name="type">The type to retrieve</param>
        /// <param name="instance">he type instance if found</param>
        /// <returns>true if the type is found, otherwise false</returns>
        public virtual bool TryGetInstance(Type type, out object instance)
        {
            if (IsRegistered(type))
            {
                instance = _services[type]();
                return true;
            }

            instance = null;
            return false;
        }
    }
}
