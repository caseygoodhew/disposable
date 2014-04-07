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
        /// Authenticates an email and password
        /// </summary>
        /// <param name="dbHelper">The dbHelper that should be used to authenticate the credentials</param>
        /// <param name="email">The email to authenticate</param>
        /// <param name="password">The corresponding password</param>
        /// <returns>true if the pair are authenticated, otherwise false</returns>
        public bool Authenticate(IDbHelper dbHelper, string email, string password)
        {
            return dbHelper.ReturnValue<bool, IUserPackage>(x => x.AuthenticateUserProcedure(email, password));
        }

        public long CreateUser(IDbHelper dbHelper, string email, string password, bool isApproved, out string confirmationCode)
        {
            throw new NotImplementedException();
        }

        private void Test<T>(Func<T, IStoredProcedure> func) where T : class
        {
            
        }
    }
}
