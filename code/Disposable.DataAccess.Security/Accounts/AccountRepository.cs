using Disposable.Common.ServiceLocator;
using Disposable.DataAccess.Packages.User;

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
            var userPackage = Locator.Current.Instance<IUserPackage>();
            return dbHelper.ReturnBool(userPackage.AuthenticateUser(username, password));
        }
    }
}
