using System;
using Disposable.Common.ServiceLocator;
using Disposable.Common.Services;
using Disposable.DataAccess.Security.Accounts;
using Disposable.Security.Accounts;
using Disposable.Security.Authentication;

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

            // repositories
            registrar.Register<IAccountRepository>(() => new AccountRepository());
            
            // business layer
            registrar.Register<IAuthentication>(() => new Authentication());
            registrar.Register<IAccountManager>(() => new AccountManager());
            
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
