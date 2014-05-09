using Disposable.Data.Access;
using Disposable.Security.Accounts;

namespace Disposable.Data.Security.Accounts
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Authenticates an email and password
        /// </summary>
        /// <param name="dbHelper">The dbHelper that should be used to authenticate the credentials</param>
        /// <param name="email">The email to authenticate</param>
        /// <param name="password">The corresponding password</param>
        /// <returns>true if the pair are authenticated, otherwise false</returns>
        bool Authenticate(IDbHelper dbHelper, string email, string password);

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="dbHelper">The dbHelper that should be used to create the user</param>
        /// <param name="email">The user's email address</param>
        /// <param name="password">The user's password</param>
        /// <param name="isApproved">Flag that the calling service has approved the user</param>
        /// <param name="confirmationCode">The output confirmation code</param>
        /// <returns>The user sid</returns>
        long CreateUser(IDbHelper dbHelper, string email, string password, bool isApproved, out string confirmationCode);

        /// <summary>
        /// Indicates whether the user has local account.
        /// </summary>
        /// <param name="dbHelper">The dbHelper that should be used to check the local account</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>true if the user has local account; otherwise, false.</returns>
        bool HasLocalAccount(IDbHelper dbHelper, long userId);

        // TODO: Depricate this method once it's removed from MVC
        IUser GetUser(IDbHelper dbHelper, string username);
    }
}
