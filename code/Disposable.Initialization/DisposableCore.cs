using System;
using Disposable.Common.ServiceLocator;
using Disposable.Common.Services;
using Disposable.DataAccess.Security;
using Disposable.DataAccess.Security.Accounts;
using Disposable.Security;
using Disposable.Security.Accounts;
using Disposable.Security.Authentication;
using Disposable.Validation;

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

            DataAccessSecurity.Register(registrar);
            Security.Security.Register(registrar);
            Validation.Validation.Register(registrar);

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
