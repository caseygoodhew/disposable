using System;

namespace Disposable.Common.ServiceLocator
{
    /// <summary>
    /// Service locator function registration services to allow product specific implementations to add additional or replace existing locator functions 
    /// </summary>
    public class OverrideRegistrar : BaseRegistrar
    {
        private readonly BaseRegistrar baseRegistrar;

        public OverrideRegistrar(BaseRegistrar baseRegistrar)
        {
            this.baseRegistrar = baseRegistrar;
        }

        /// <summary>
        /// Tries to get the implementation of a given <see cref="Type"/>
        /// </summary>
        /// <param name="type">The type to retrieve</param>
        /// <param name="instance">he type instance if found</param>
        /// <returns>true if the type is found, otherwise false</returns>
        public override bool TryGetInstance(Type type, out object instance)
        {
            if (services.ContainsKey(type))
            {
                return base.TryGetInstance(type, out instance);
            }

            return baseRegistrar.TryGetInstance(type, out instance);
        }

        /// <summary>
        /// Checks to see if a locator function for given <see cref="Type"/> is already registered
        /// </summary>
        /// <param name="type">The type to look for</param>
        /// <returns>true if the <see cref="type"/> is registered</returns>
        public override bool IsRegistered(Type type)
        {
            return services.ContainsKey(type) || baseRegistrar.IsRegistered(type);
        }
    }
}
