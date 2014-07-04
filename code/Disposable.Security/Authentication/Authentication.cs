using System;
using Disposable.Common.ServiceLocator;
using Disposable.Data.Access;
using Disposable.Data.Security.Accounts;

namespace Disposable.Security.Authentication
{
    /// <summary>
    /// Provides authentication services.
    /// </summary>
    public class Authentication : IAuthentication
    {
        private readonly Lazy<IAccountRepository> accountRepository = new Lazy<IAccountRepository>(() => Locator.Current.Instance<IAccountRepository>());
        
        /// <summary>
        /// Authenticates a username (email address), password and device.
        /// </summary>
        /// <param name="username">The username (email address) to authenticate.</param>
        /// <param name="password">The password.</param>
        /// <param name="deviceGuid">The Guid of the device.</param>
        /// <returns>The authentication result.</returns>
        public AuthenticationStatus Authenticate(string username, string password, Guid deviceGuid)
        {
            using (IDbHelper dbHelper = new DbHelper())
            {
                if (this.accountRepository.Value.Authenticate(dbHelper, username, password))
                {
                    return AuthenticationStatus.Succeeded;
                }

                return AuthenticationStatus.Failed;
            }
        }
    }
}
