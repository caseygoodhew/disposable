using System;

namespace Disposable.Security.Accounts
{
    public interface IUser
    {
        DateTime CreationDate { get; }
        
        string Email { get; }
        
        string IsApproved { get; }
        
        string IsLocked { get; }
        
        DateTime LastActivityDate { get; }
        
        DateTime LastLockoutDate { get; }
        
        DateTime LastLoginDate { get; }
        
        DateTime LastPasswordChangedDate { get; }
    }
}
