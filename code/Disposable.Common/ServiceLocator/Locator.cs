using System;
using System.Linq;

namespace Disposable.Common.ServiceLocator
{
    /// <summary>
    /// Service Locator, Singleton, IOC
    /// </summary>
    public class Locator : ILocator
    {
        private static readonly Lazy<ILocator> LocatorInstance = new Lazy<ILocator>(() => new Locator());

        private bool _initialized;

        private Locator()
        {
            BaseRegistrar = new BaseRegistrar();
            OverrideRegistrar = new OverrideRegistrar(BaseRegistrar);
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

        /// <summary>
        /// This method allows an external service to call into this function one time only in order to register it's services with the base service locator.
        /// </summary>
        /// <param name="initializers">The list of initializsers to call.</param>
        public void Initialize(params Action<IRegistrar>[] initializers)
        {
            if (_initialized)
            {
                throw new InvalidOperationException("Already initialized");
            }

            // flag as initialized early to prevent Actions from recalling Initialize (malicious or accidental endless loop)
            _initialized = true;

            initializers.ToList().ForEach(x => x.Invoke(BaseRegistrar));
        }
        
        /// <summary>
        /// Gets or sets the base registrar
        /// </summary>
        /// <remarks>'set' intentionally left internal for unit testing</remarks>
        internal BaseRegistrar BaseRegistrar { get; set; }

        /// <summary>
        /// Gets or sets the base registrar
        /// </summary>
        /// <remarks>'set' intentionally left internal for unit testing</remarks>
        internal BaseRegistrar OverrideRegistrar { get; set; }

        /// <summary>
        /// Gets the implementation of type T
        /// </summary>
        /// <typeparam name="T">The type to retrieve</typeparam>
        /// <returns>The type instance</returns>
        public T Instance<T>() where T : class
        {
            return OverrideRegistrar.Instance<T>();
        }

        /// <summary>
        /// Gets the implementation of type T from the Current context.
        /// </summary>
        /// <typeparam name="T">The type to retrieve</typeparam>
        /// <returns>The type instance</returns>
        public static T Get<T>() where T : class
        {
            return Current.Instance<T>();
        }

        /// <summary>
        /// Gets a new Lazy loader for the implementation of type T from the Current context.
        /// </summary>
        /// <typeparam name="T">The type to retrieve</typeparam>
        /// <returns>The type instance</returns>
        public static Lazy<T> Lazy<T>() where T : class
        {
            return new Lazy<T>(Get<T>);
        }

        /// <summary>
        /// Gets the implementation of a given <see cref="Type"/>
        /// </summary>
        /// <param name="type">The type to retrieve</param>
        /// <returns>The type instance</returns>
        public object Instance(Type type)
        {
            return OverrideRegistrar.Instance(type);
        }

        /// <summary>
        /// Tries to get the implementation of type T
        /// </summary>
        /// <typeparam name="T">The type to try to retrieve</typeparam>
        /// <param name="instance">The type instance if found</param>
        /// <returns>true if the type is found, otherwise false</returns>
        public bool TryGetInstance<T>(out T instance) where T : class
        {
            return OverrideRegistrar.TryGetInstance<T>(out instance);
        }

        /// <summary>
        /// Tries to get the implementation of a given <see cref="Type"/>
        /// </summary>
        /// <param name="type">The type to retrieve</param>
        /// <param name="instance">he type instance if found</param>
        /// <returns>true if the type is found, otherwise false</returns>
        public bool TryGetInstance(Type type, out object instance)
        {
            return OverrideRegistrar.TryGetInstance(type, out instance);
        }

        /// <summary>
        /// Registers a locator function of type T
        /// </summary>
        /// <typeparam name="T">The generic type to register (typically an interface)</typeparam>
        /// <param name="locatorFunc">A function that returns an instance of T</param>
        public void Register<T>(Func<T> locatorFunc) where T : class
        {
            OverrideRegistrar.Register(locatorFunc);
        }
    }
}
