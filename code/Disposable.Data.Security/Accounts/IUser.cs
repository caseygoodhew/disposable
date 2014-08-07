using System;

namespace Disposable.Data.Security.Accounts
{
    /// <summary>
    /// Interface of a basic user account
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets the date that the user was created.
        /// </summary>
        DateTime CreationDate { get; }
        
        /// <summary>
        /// Gets the user's email address.
        /// </summary>
        string Email { get; }
        
        /// <summary>
        /// Gets a value indicating whether the user account is approved.
        /// </summary>
        bool IsApproved { get; }

        /// <summary>
        /// Gets a value indicating whether the user account is locked.
        /// </summary>
        bool IsLocked { get; }
        
        /// <summary>
        /// Gets the user's last activity date.
        /// </summary>
        DateTime LastActivityDate { get; }

        /// <summary>
        /// Gets the user's last lockout date.
        /// </summary>
        DateTime LastLockoutDate { get; }

        /// <summary>
        /// Gets the user's last login date.
        /// </summary>
        DateTime LastLoginDate { get; }

        /// <summary>
        /// Gets the user's last password change date.
        /// </summary>
        DateTime LastPasswordChangedDate { get; }
    }
}
