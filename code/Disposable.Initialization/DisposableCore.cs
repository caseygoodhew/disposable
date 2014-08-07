using Disposable.Common.ServiceLocator;
using Disposable.Common.Services;
using Disposable.Validation;
using System;

namespace Disposable.Initialization
{
    /// <summary>
    /// Singleton to register all core services with the service locator.
    /// </summary>
    public class DisposableCore
    {
        private static readonly Lazy<DisposableCore> DisposableCoreInstance = new Lazy<DisposableCore>(() => new DisposableCore());

        private DisposableCore()
        {
            var locator = Locator.Current as Locator;
            
            if (locator == null)
            {
                throw new InvalidOperationException();
            }

            var registrar = locator.BaseRegistrar;
            
            Data.Registration.Register(registrar);
            Data.Map.Registration.Register(registrar);
            Data.Security.Registration.Register(registrar);
            
            Security.Registration.Register(registrar);
            
            Registration.Register(registrar);

            // misc
            registrar.Register<ITimeSource>(() => new LocalTimeSource());
        }

        /// <summary>
        /// Initializes the Core services.
        /// </summary>
        /// <returns>Flag indicating that the core started successfully.</returns>
        public static bool Initialize()
        {
            return DisposableCoreInstance.Value != null;
        }
    }
}
