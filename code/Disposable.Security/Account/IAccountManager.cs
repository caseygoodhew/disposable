namespace Disposable.Security.Account
{
    public interface IAccountManager
    {
        void CreateUser();
        void UpdateUser();
        void DeleteUser();
        void FindUsers();
        void GetAllUsers();
        void GetUser();
        void ApproveUser();
        void LockUser();
        void UnlockUser();
        void ChangePassword();
    }
}
