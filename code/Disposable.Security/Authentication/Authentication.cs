using System;
using Disposable.Common.ServiceLocator;
using Disposable.Data.Access;
using Disposable.Data.Security.Accounts;

namespace Disposable.Security.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class Authentication : IAuthentication
    {
        private readonly Lazy<IAccountRepository> _accountRepository = new Lazy<IAccountRepository>(() => Locator.Current.Instance<IAccountRepository>());
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="deviceGuid"></param>
        /// <returns></returns>
        public AuthenticationResult Authenticate(string username, string password, Guid deviceGuid)
        {
            using (IDbHelper dbHelper = new DbHelper())
            {
                if (_accountRepository.Value.Authenticate(dbHelper, username, password))
                {
                    return new AuthenticationResult(AuthenticationStatus.Succeeded);
                }

                return new AuthenticationResult(AuthenticationStatus.Failed);
            }
        }
    }
}
