using System;

namespace Disposable.Common.ServiceLocator
{
    /// <summary>
    /// Service locator function instance location services
    /// </summary>
    public interface ILocator
    {
        /// <summary>
        /// Gets the implementation of type T
        /// </summary>
        /// <typeparam name="T">The type to retrieve</typeparam>
        /// <returns>The type instance</returns>
        T Instance<T>() where T : class;

        /// <summary>
        /// Gets the implementation of a given <see cref="Type"/>
        /// </summary>
        /// <param name="type">The type to retrieve</param>
        /// <returns>The type instance</returns>
        object Instance(Type type);

        /// <summary>
        /// Tries to get the implementation of type T
        /// </summary>
        /// <typeparam name="T">The type to try to retrieve</typeparam>
        /// <param name="instance">The type instance if found</param>
        /// <returns>true if the type is found, otherwise false</returns>
        bool TryGetInstance<T>(out T instance) where T : class;

        /// <summary>
        /// Tries to get the implementation of a given <see cref="Type"/>
        /// </summary>
        /// <param name="type">The type to retrieve</param>
        /// <param name="instance">he type instance if found</param>
        /// <returns>true if the type is found, otherwise false</returns>
        bool TryGetInstance(Type type, out object instance);
    }
}
