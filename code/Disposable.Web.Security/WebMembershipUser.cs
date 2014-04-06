using System;
using System.Web.Security;
using Disposable.Web.Security.System;

namespace Disposable.Web.Security
{
    /// <summary>
    /// Implementation of the the <see cref="MembershipUser"/> for web requests
    /// </summary>
    public class WebMembershipUser : MembershipUser, ISystemMembershipUser
    {
        /// <summary>
        /// Gets or sets application-specific information for the membership user.
        /// </summary>
        /// <returns>
        /// Application-specific information for the membership user.
        /// </returns>
        public override string Comment { get; set; }

        /// <summary>
        /// Gets the date and time when the user was added to the membership data store.
        /// </summary>
        /// <returns>
        /// The date and time when the user was added to the membership data store.
        /// </returns>
        public override DateTime CreationDate
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets or sets the e-mail address for the membership user.
        /// </summary>
        /// <returns>
        /// The e-mail address for the membership user.
        /// </returns>
        public override string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the membership user can be authenticated.
        /// </summary>
        /// <returns>
        /// true if the user can be authenticated; otherwise, false.
        /// </returns>
        public override bool IsApproved { get; set; }

        /// <summary>
        /// Gets a value indicating whether the membership user is locked out and unable to be validated.
        /// </summary>
        /// <returns>
        /// true if the membership user is locked out and unable to be validated; otherwise, false.
        /// </returns>
        public override bool IsLockedOut
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a value indicating whether the user is currently online.
        /// </summary>
        /// <returns>
        /// true if the user is online; otherwise, false.
        /// </returns>
        public override bool IsOnline
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets or sets the date and time when the membership user was last authenticated or accessed the application.
        /// </summary>
        /// <returns>
        /// The date and time when the membership user was last authenticated or accessed the application.
        /// </returns>
        public override DateTime LastActivityDate { get; set; }

        /// <summary>
        /// Gets the most recent date and time that the membership user was locked out.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.DateTime"/> object that represents the most recent date and time that the membership user was locked out.
        /// </returns>
        public override DateTime LastLockoutDate
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets or sets the date and time when the user was last authenticated.
        /// </summary>
        /// <returns>
        /// The date and time when the user was last authenticated.
        /// </returns>
        public override DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Gets the date and time when the membership user's password was last updated.
        /// </summary>
        /// <returns>
        /// The date and time when the membership user's password was last updated.
        /// </returns>
        public override DateTime LastPasswordChangedDate
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the password question for the membership user.
        /// </summary>
        /// <returns>
        /// The password question for the membership user.
        /// </returns>
        public override string PasswordQuestion
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the name of the membership provider that stores and retrieves user information for the membership user.
        /// </summary>
        /// <returns>
        /// The name of the membership provider that stores and retrieves user information for the membership user.
        /// </returns>
        public override string ProviderName
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the user identifier from the membership data source for the user.
        /// </summary>
        /// <returns>
        /// The user identifier from the membership data source for the user.
        /// </returns>
        public override object ProviderUserKey
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the logon name of the membership user.
        /// </summary>
        /// <returns>
        /// The logon name of the membership user.
        /// </returns>
        public override string UserName
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Updates the password for the membership user in the membership data store.
        /// </summary>
        /// <param name="oldPassword">The current password for the membership user.</param>
        /// <param name="newPassword">The new password for the membership user.</param>
        /// <returns>
        /// true if the update was successful; otherwise, false.
        /// </returns>
        public override bool ChangePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the password question and answer for the membership user in the membership data store.
        /// </summary>
        /// <param name="password">The current password for the membership user.</param>
        /// <param name="newPasswordQuestion">The new password question value for the membership user.</param>
        /// <param name="newPasswordAnswer">The new password answer value for the membership user.</param>
        /// <returns>
        /// true if the update was successful; otherwise, false.
        /// </returns>
        public override bool ChangePasswordQuestionAndAnswer(string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the password for the membership user from the membership data store.
        /// </summary>
        /// <returns>
        /// The password for the membership user.
        /// </returns>
        public override string GetPassword()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the password for the membership user from the membership data store.
        /// </summary>
        /// <param name="passwordAnswer">The password answer for the membership user.</param>
        /// <returns>
        /// The password for the membership user.
        /// </returns>
        /// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
        public override string GetPassword(string passwordAnswer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// <returns>
        /// The new password for the membership user.
        /// </returns>
        public override string ResetPassword()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// <param name="passwordAnswer">The password answer for the membership user.</param>
        /// <returns>
        /// The new password for the membership user.
        /// </returns>
        public override string ResetPassword(string passwordAnswer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears the locked-out state of the user so that the membership user can be validated.
        /// </summary>
        /// <returns>
        /// true if the membership user was successfully unlocked; otherwise, false.
        /// </returns>
        public override bool UnlockUser()
        {
            throw new NotImplementedException();
        }
    }
}
