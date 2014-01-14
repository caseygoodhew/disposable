using System;
using Disposable.Common.ServiceLocator;
using Disposable.Common.Services;
using Disposable.DataAccess;

namespace Disposable.Initialization
{
    /// <summary>
    /// 
    /// </summary>
    public class DisposableCore
    {
        private static readonly Lazy<DisposableCore> DisposableCoreInstance = new Lazy<DisposableCore>(() => new DisposableCore());

        private DisposableCore()
        {
            var locator = (Locator.Current as Locator);
            
            if (locator == null)
            {
                throw new InvalidOperationException();
            }

            var registrar = locator.BaseRegistrar;

            
            Disposable.DataAccess.
            DataAccess.Packages.Registrar.Register(registrar);
            DataAccess.Security.Registrar.Register(registrar);
            
            Security.Registrar.Register(registrar);
            
            Validation.Registrar.Register(registrar);

            // misc
            registrar.Register<ITimeSource>(() => new LocalTimeSource());
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool Initialize()
        {
            return DisposableCoreInstance.Value != null;
        }
    }
}
