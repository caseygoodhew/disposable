using Disposable.DataAccess.Security.Accounts.StoredProcedures;

namespace Disposable.DataAccess.Security.Accounts
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
            return dbHelper.ReturnBool<AuthenticateUserProcedure>(AuthenticateUserProcedure.Parameterize(username, password));
        }
    }
}
