using System;

namespace Disposable.Common.ServiceLocator
{
    /// <summary>
    /// Service Locator, Singleton, IOC
    /// </summary>
    public class Locator : ILocator
    {
        private static readonly Lazy<ILocator> LocatorInstance = new Lazy<ILocator>(() => new Locator());

        private Locator()
        {
            BaseRegistrar = new BaseRegistrar();
            OverrideRegistrar = new OverrideRegistrar();
        }

        /// <summary>
        /// Gets the current locator
        /// </summary>
        public static ILocator Current
        {
            get
            {
                return LocatorInstance.Value;
            }
        }

        // set intentionally left internal for unit testing
        internal IRegistrar BaseRegistrar { get; set; }

        // set intentionally left internal for unit testing
        internal IRegistrar OverrideRegistrar { get; set; }

        /// <summary>
        /// Gets the implementation of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Instance<T>() where T : class
        {
            return OverrideRegistrar.Instance<T>();
        }

        /// <summary>
        /// Registers a locator function of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="locatorFunc">A function that returns an instance of T</param>
        public void Register<T>(Func<T> locatorFunc) where T : class
        {
            OverrideRegistrar.Register(locatorFunc);
        }
    }
}
