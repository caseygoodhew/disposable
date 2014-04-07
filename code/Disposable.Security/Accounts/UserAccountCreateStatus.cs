namespace Disposable.Security.Accounts
{
    public enum UserAccountCreateStatus
    {
        DuplicateEmail,
        //DuplicateProviderUserKey.
        InvalidEmail,
        InvalidPassword,
        //InvalidProviderUserKey,
        Success,
        //UserRejected
    }
}
