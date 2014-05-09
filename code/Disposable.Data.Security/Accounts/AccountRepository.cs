using System;
using System.Data;
using System.Linq.Expressions;
using Disposable.Common.ServiceLocator;
using Disposable.Data.Access;
using Disposable.Data.Packages.Core;
using Disposable.Data.Packages.User;
using Disposable.Security.Accounts;

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
            long userSid;
            Guid confirmationGuid;
            
            dbHelper.Run<IUserPackage, long, Guid>(x => x.CreateUserProcedure(email, password, isApproved), out userSid, out confirmationGuid);

            confirmationCode = confirmationGuid.ToString();
            return userSid;
        }

        public bool HasLocalAccount(IDbHelper dbHelper, long userId)
        {
            throw new NotImplementedException();

            //return dbHelper.ReturnValue<bool, IUserPackage>(x => x.HasLocalAccount(userId));
        }

        public IUser GetUser(IDbHelper dbHelper, string username)
        {
            var test = dbHelper.ReturnValue<DataSet, IUserPackage>(x => x.GetUserProcedure(username));
            throw new NotImplementedException();
        }
    }
}
