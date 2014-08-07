using Disposable.Data.Access;
using Disposable.Data.Security.Packages.User;
using System;
using System.Data;

namespace Disposable.Data.Security.Accounts
{
    /// <summary>
    /// Repository wrapper for user account related activities
    /// </summary>
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

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="dbHelper">The dbHelper that should be used to create the user</param>
        /// <param name="email">The user's email address</param>
        /// <param name="password">The user's password</param>
        /// <param name="isApproved">Flag that the calling service has approved the user</param>
        /// <param name="confirmationCode">The output confirmation code</param>
        /// <returns>The user sid</returns>
        public long CreateUser(IDbHelper dbHelper, string email, string password, bool isApproved, out string confirmationCode)
        {
            long userSid;
            Guid confirmationGuid;
            
            dbHelper.Run<IUserPackage, long, Guid>(x => x.CreateUserProcedure(email, password, isApproved), out userSid, out confirmationGuid);

            confirmationCode = confirmationGuid.ToString();
            return userSid;
        }

        /// <summary>
        /// Indicates whether the user has local account.
        /// </summary>
        /// <param name="dbHelper">The dbHelper that should be used to check the local account</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>true if the user has local account; otherwise, false.</returns>
        public bool HasLocalAccount(IDbHelper dbHelper, long userId)
        {
            throw new NotImplementedException();

            ////return dbHelper.ReturnValue<bool, IUserPackage>(x => x.HasLocalAccount(userId));
        }

        /// <summary>
        /// To be deprecated
        /// </summary>
        /// <param name="dbHelper">To be deprecated</param>
        /// <param name="username">To be deprecated</param>
        /// <returns>To be deprecated</returns>
        [Obsolete("Remove this method once it's removed from MVC")]
        public IUser GetUser(IDbHelper dbHelper, string username)
        {
            var test = dbHelper.ReturnValue<DataSet, IUserPackage>(x => x.GetUserProcedure(username));
            throw new NotImplementedException();
        }
    }
}
