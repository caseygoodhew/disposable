using System;
using System.Collections.Generic;
using Disposable.Common.ServiceLocator;
using Disposable.Data.Access;
using Disposable.Data.Security.Accounts;
using Disposable.Data.Security.Accounts.Exceptions;

namespace Disposable.Security.Accounts
{
    public class AccountManager : IAccountManager
    {
        private readonly Lazy<IAccountRepository> _accountRepository = new Lazy<IAccountRepository>(() => Locator.Current.Instance<IAccountRepository>());
        
        public bool ApproveUser()
        {
            throw new System.NotImplementedException();
        }

        public bool ChangePassword()
        {
            throw new System.NotImplementedException();
        }

        public long CreateUser(string email, string password, bool isApproved, out UserAccountCreateStatus status, out string confirmationCode)
        {
            confirmationCode = "";
            
            using (IDbHelper dbHelper = new DbHelper())
            {
                try
                {
                    var userSid = _accountRepository.Value.CreateUser(dbHelper, email, password, isApproved, out confirmationCode);
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

        public bool DeleteUser()
        {
            throw new System.NotImplementedException();
        }

        public IList<IUser> FindUsers()
        {
            throw new System.NotImplementedException();
        }

        public IList<IUser> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public IUser GetUser()
        {
            throw new System.NotImplementedException();
        }

        public bool LockUser()
        {
            throw new System.NotImplementedException();
        }

        public bool UnlockUser()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser()
        {
            throw new System.NotImplementedException();
        }
    }
}
