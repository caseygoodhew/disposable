using System.Collections.Generic;

namespace Disposable.Security.Accounts
{
    public interface IAccountManager
    {
        // ?? This is my own creation
        bool ApproveUser();

        // string username, string oldPassword, string newPassword
        bool ChangePassword();
        
        // string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status
        IUser CreateUser();

        // string username, bool deleteAllRelatedData
        bool DeleteUser();

        // string emailToMatch, int pageIndex, int pageSize, out int totalRecords
        // string usernameToMatch, int pageIndex, int pageSize, out int totalRecords
        IList<IUser> FindUsers();

        // int pageIndex, int pageSize, out int totalRecords
        IList<IUser> GetAllUsers();

        // object providerUserKey, bool userIsOnline
        IUser GetUser();

        // ?? This is my own creation
        bool LockUser();

        // string userName
        bool UnlockUser();

        // MembershipUser user
        void UpdateUser();
    }
}
