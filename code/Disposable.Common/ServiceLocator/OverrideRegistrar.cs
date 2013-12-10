namespace Disposable.Common.ServiceLocator
{
    /// <summary>
    /// Service locator function registration services to allow product specific implementations to add additional or replace existing locator functions 
    /// </summary>
    public class OverrideRegistrar : BaseRegistrar
    {
        /// <summary>
        /// Gets an instance of type T, first attempting to retrieve the type locally, and then by falling back to the <see cref="Locator"/>'s <see cref="CoreRegistrar"/>
        /// </summary>
        /// <typeparam name="T">The generic type to look for</typeparam>
        /// <returns></returns>
        /// <exception cref="ServiceNotFoundException">Thrown when a locator function for the given generic type T is not found</exception>
        public override T Instance<T>()
        {
            if (IsRegistered<T>())
            {
                return base.Instance<T>();
            }

            var locator = Locator.Current as Locator;
            
            if (locator == null)
            {
                throw new ServiceNotFoundException(typeof(T));
            }
            
            return locator.BaseRegistrar.Instance<T>();
        }
    }
}
