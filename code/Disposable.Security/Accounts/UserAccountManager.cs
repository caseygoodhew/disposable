using System;
using System.Collections.Generic;
using Disposable.Common.ServiceLocator;
using Disposable.Data.Access;
using Disposable.Data.Security.Accounts;
using Disposable.Data.Security.Accounts.Exceptions;

namespace Disposable.Security.Accounts
{
    /// <summary>
    /// Provides user account management services.
    /// </summary>
    public class UserAccountManager : IUserAccountManager
    {
        private readonly Lazy<IAccountRepository> accountRepository = new Lazy<IAccountRepository>(() => Locator.Current.Instance<IAccountRepository>());
        
        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        public bool ApproveUser()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        public bool ChangePassword()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password (empty passwords will be set to a securely random value)</param>
        /// <param name="isApproved">Sets the initial approval state for the user.</param>
        /// <param name="status">The resultant status of the account creation.</param>
        /// <param name="confirmationCode">A confirmation code that can be used to validate a user's email address.</param>
        /// <returns>The user id.</returns>
        public long CreateUser(string email, string password, bool isApproved, out UserAccountCreateStatus status, out string confirmationCode)
        {
            confirmationCode = string.Empty;
            
            using (IDbHelper dbHelper = new DbHelper())
            {
                try
                {
                    var userSid = accountRepository.Value.CreateUser(dbHelper, email, password, isApproved, out confirmationCode);
                    status = UserAccountCreateStatus.Success;
                    return userSid;
                }
                catch (InvalidPasswordException)
                {
                    status = UserAccountCreateStatus.InvalidPassword;
                }
                catch (InvalidEmailException)
                {
                    status = UserAccountCreateStatus.InvalidEmail;
                }
                catch (DuplicateEmailException)
                {
                    status = UserAccountCreateStatus.DuplicateEmail;
                }
            }

            return 0;
        }

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        public bool DeleteUser()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        public IList<IUser> FindUsers()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        public IList<IUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the user account by username. 
        /// </summary>
        /// <param name="username">The username (email address) of the user.</param>
        /// <returns>The user's details</returns>
        /// <remarks>TODO: Deprecate</remarks>
        public IUser GetUser(string username)
        {
            using (IDbHelper dbHelper = new DbHelper())
            {
                return accountRepository.Value.GetUser(dbHelper, username);
            }
        }

        /// <summary>
        /// Determines if the user account exists locally.
        /// </summary>
        /// <param name="userId">The used id to check.</param>
        /// <returns>true if the user account exists locally.</returns>
        public bool HasLocalAccount(long userId)
        {
            using (IDbHelper dbHelper = new DbHelper())
            {
                return accountRepository.Value.HasLocalAccount(dbHelper, userId);
            }
        }

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        public bool LockUser()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        public bool UnlockUser()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        public void UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}
