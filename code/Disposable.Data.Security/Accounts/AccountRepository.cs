using System;
using System.Linq.Expressions;
using Disposable.Common.ServiceLocator;
using Disposable.Data.Access;
using Disposable.Data.Packages.Core;
using Disposable.Data.Packages.User;

namespace Disposable.Data.Security.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// Authenticates a username and password
        /// </summary>
        /// <param name="dbHelper">The dbHelper that should be used to authenticate the credentials</param>
        /// <param name="username">The username to authenticate</param>
        /// <param name="password">The corresponding password</param>
        /// <returns>true if the pair are authenticated, otherwise false</returns>
        public bool Authenticate(IDbHelper dbHelper, string username, string password)
        {
            return dbHelper.ReturnValue<bool, IUserPackage>(x => x.AuthenticateUserFunction(username, password));
        }

        private void Test<T>(Func<T, IStoredProcedure> func) where T : class
        {
            
        }
    }
}
