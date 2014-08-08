using Disposable.Common.ServiceLocator;
using Disposable.Common.Services;
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

            locator.Initialize(
                Data.Registration.Register,
                Data.Map.Registration.Register,
                Data.Security.Registration.Register,
                Security.Registration.Register,
                Validation.Registration.Register,
                Common.Registration.Register
            );
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
