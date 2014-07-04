namespace Disposable.Security.Accounts
{
    /// <summary>
    /// User account create status flags
    /// </summary>
    public enum UserAccountCreateStatus
    {
        DuplicateEmail,
        ////DuplicateProviderUserKey.
        InvalidEmail,
        InvalidPassword,
        ////InvalidProviderUserKey,
        Success,
        ////UserRejected
    }
}
