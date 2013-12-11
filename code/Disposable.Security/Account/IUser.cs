using System;

namespace Disposable.Security.Account
{
    public interface IUser
    {
        DateTime CreationDate { get; }
        
        string Email { get; }
        
        string IsApproved { get; }
        
        string IsLockedOut { get; }
        
        DateTime LastActivityDate { get; }
        
        DateTime LastLockoutDate { get; }
        
        DateTime LastLoginDate { get; }
        
        DateTime LastPasswordChangedDate { get; }
    }
}
