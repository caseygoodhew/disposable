using System;

namespace Disposable.Common.ServiceLocator
{
    /// <summary>
    /// Service locator function registration services to allow product specific implementations to add additional or replace existing locator functions 
    /// </summary>
    public class OverrideRegistrar : BaseRegistrar
    {
        /// <summary>
        /// Tries to get the implementation of type T
        /// </summary>
        /// <typeparam name="T">The type to try to retrieve</typeparam>
        /// <param name="instance">The type instance if found</param>
        /// <returns>true if the type is found, otherwise false</returns>
        public override bool TryGetInstance<T>(out T instance)
        {
            if (IsRegistered<T>())
            {
                return base.TryGetInstance(out instance);
            }

            var locator = Locator.Current as Locator;

            if (locator == null)
            {
                throw new ServiceNotFoundException(typeof(T));
            }

            return locator.BaseRegistrar.TryGetInstance(out instance);
        }

        /// <summary>
        /// Tries to get the implementation of a given <see cref="Type"/>
        /// </summary>
        /// <param name="type">The type to retrieve</param>
        /// <param name="instance">he type instance if found</param>
        /// <returns>true if the type is found, otherwise false</returns>
        public override bool TryGetInstance(Type type, out object instance)
        {
            if (IsRegistered(type))
            {
                return base.TryGetInstance(type, out instance);
            }

            var locator = Locator.Current as Locator;

            if (locator == null)
            {
                throw new ServiceNotFoundException(type);
            }

            return locator.BaseRegistrar.TryGetInstance(type, out instance);
        }
    }
}
