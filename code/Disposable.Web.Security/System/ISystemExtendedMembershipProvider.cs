using System;
using System.Collections.Generic;
using System.Web.Security;
using WebMatrix.WebData;

namespace Disposable.Web.Security.System
{
    public interface ISystemExtendedMembershipProvider
    {
        /// <summary>
        /// Gets or sets the name of the application using the custom membership provider.
        /// </summary>
        /// <returns>
        /// The name of the application using the custom membership provider.
        /// </returns>
        string ApplicationName { get; set; }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to allow users to reset their passwords.
        /// </summary>
        /// <returns>
        /// true if the membership provider supports password reset; otherwise, false. The default is true.
        /// </returns>
        bool EnablePasswordReset { get; }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to allow users to retrieve their passwords.
        /// </summary>
        /// <returns>
        /// true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.
        /// </returns>
        bool EnablePasswordRetrieval { get; }

        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        /// <returns>
        /// The number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </returns>
        int MaxInvalidPasswordAttempts { get; }

        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        /// <returns>
        /// The minimum number of special characters that must be present in a valid password.
        /// </returns>
        int MinRequiredNonAlphanumericCharacters { get; }

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        /// <returns>
        /// The minimum length required for a password. 
        /// </returns>
        int MinRequiredPasswordLength { get; }

        /// <summary>
        /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </summary>
        /// <returns>
        /// The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </returns>
        int PasswordAttemptWindow { get; }

        /// <summary>
        /// Gets a value indicating the format for storing passwords in the membership data store.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.Web.Registration.MembershipPasswordFormat"/> values indicating the format for storing passwords in the data store.
        /// </returns>
        MembershipPasswordFormat PasswordFormat { get; }

        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        /// <returns>
        /// A regular expression used to evaluate a password.
        /// </returns>
        string PasswordStrengthRegularExpression { get; }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
        /// </summary>
        /// <returns>
        /// true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.
        /// </returns>
        bool RequiresQuestionAndAnswer { get; }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
        /// </summary>
        /// <returns>
        /// true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.
        /// </returns>
        bool RequiresUniqueEmail { get; }

        /// <summary>
        /// Processes a request to update the password for a membership user.
        /// </summary>
        /// <param name="username">The user to update the password for. </param>
        /// <param name="oldPassword">The current password for the specified user. </param>
        /// <param name="newPassword">The new password for the specified user. </param>
        /// <returns>
        /// true if the password was updated successfully; otherwise, false.
        /// </returns>
        bool ChangePassword(string username, string oldPassword, string newPassword);
        
        /// <summary>
        /// Processes a request to update the password question and answer for a membership user.
        /// </summary>
        /// <param name="username">The user to change the password question and answer for. </param>
        /// <param name="password">The password for the specified user. </param>
        /// <param name="newPasswordQuestion">The new password question for the specified user. </param>
        /// <param name="newPasswordAnswer">The new password answer for the specified user. </param>
        /// <returns>
        /// true if the password question and answer are updated successfully; otherwise, false.
        /// </returns>
        bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer);

        /// <summary>
        /// Activates a pending membership account for the specified user.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="accountConfirmationToken">A confirmation token to pass to the authentication provider.</param>
        /// <returns>
        /// true if the account is confirmed; otherwise, false.
        /// </returns>
        bool ConfirmAccount(string userName, string accountConfirmationToken);

        /// <summary>
        /// Activates a pending membership account.
        /// </summary>
        /// <param name="accountConfirmationToken">A confirmation token to pass to the authentication provider.</param>
        /// <returns>
        /// true if the account is confirmed; otherwise, false.
        /// </returns>
        bool ConfirmAccount(string accountConfirmationToken);

        /// <summary>
        /// When overridden in a derived class, creates a new user account using the specified user name and password, optionally requiring that the new account must be confirmed before the account is available for use.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <param name="requireConfirmationToken">(Optional) true to specify that the account must be confirmed; otherwise, false. The default is false.</param>
        /// <returns>
        /// A token that can be sent to the user to confirm the account.
        /// </returns>
        string CreateAccount(string userName, string password, bool requireConfirmationToken);

        /// <summary>
        /// When overridden in a derived class, creates a new user profile and a new membership account.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <param name="requireConfirmation">(Optional) true to specify that the user account must be confirmed; otherwise, false. The default is false.</param>
        /// <param name="values">(Optional) A dictionary that contains additional user attributes to store in the user profile. The default is null.</param>
        /// <returns>
        /// A token that can be sent to the user to confirm the user account.
        /// </returns>
        string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values);

        /// <summary>
        /// Adds a new membership user to the data source.
        /// </summary>
        /// <param name="username">The user name for the new user. </param>
        /// <param name="password">The password for the new user. </param>
        /// <param name="email">The e-mail address for the new user.</param>
        /// <param name="passwordQuestion">The password question for the new user.</param>
        /// <param name="passwordAnswer">The password answer for the new user</param>
        /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
        /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
        /// <param name="status">A <see cref="T:System.Web.Registration.MembershipCreateStatus"/> enumeration value indicating whether the user was created successfully.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Registration.MembershipUser"/> object populated with the information for the newly created user.
        /// </returns>
        MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status);

        /// <summary>
        /// When overridden in a derived class, deletes the specified membership account.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>
        /// true if the user account was deleted; otherwise, false.
        /// </returns>
        bool DeleteAccount(string userName);

        /// <summary>
        /// Removes a user from the membership data source. 
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
        /// <returns>
        /// true if the user was successfully deleted; otherwise, false.
        /// </returns>
        bool DeleteUser(string username, bool deleteAllRelatedData);

        /// <summary>
        /// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
        /// </summary>
        /// <param name="emailToMatch">The e-mail address to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Registration.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Registration.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords);

        /// <summary>
        /// Gets a collection of membership users where the user name contains the specified user name to match.
        /// </summary>
        /// <param name="usernameToMatch">The user name to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Registration.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Registration.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);

        /// <summary>
        /// When overridden in a derived class, generates a password reset token that can be sent to a user in email.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="tokenExpirationInMinutesFromNow">(Optional) The time, in minutes, until the password reset token expires. The default is 1440 (24 hours).</param>
        /// <returns>
        /// A token to send to the user.
        /// </returns>
        string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow);

        /// <summary>
        /// When overridden in a derived class, returns all OAuth membership accounts associated with the specified user name.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>
        /// A list of all OAuth membership accounts associated with the specified user name.
        /// </returns>
        ICollection<OAuthAccountData> GetAccountsForUser(string userName);

        /// <summary>
        /// Gets a collection of all the users in the data source in pages of data.
        /// </summary>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Registration.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Registration.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords);

        /// <summary>
        /// When overridden in a derived class, returns the date and time when the specified user account was created.
        /// </summary>
        /// <param name="userName">The user name of the account.</param>
        /// <returns>
        /// The date and time the account was created, or <see cref="F:System.DateTime.MinValue"/> if the account creation date is not available.
        /// </returns>
        DateTime GetCreateDate(string userName);

        /// <summary>
        /// When overridden in a derived class, returns the date and time when an incorrect password was most recently entered for the specified user account.
        /// </summary>
        /// <param name="userName">The user name of the account.</param>
        /// <returns>
        /// The date and time when an incorrect password was most recently entered for this user account, or <see cref="F:System.DateTime.MinValue"/> if an incorrect password has not been entered for this user account.
        /// </returns>
        DateTime GetLastPasswordFailureDate(string userName);

        /// <summary>
        /// Gets the number of users currently accessing the application.
        /// </summary>
        /// <returns>
        /// The number of users currently accessing the application.
        /// </returns>
        int GetNumberOfUsersOnline();

        /// <summary>
        /// Gets the password for the specified user name from the data source.
        /// </summary>
        /// <param name="username">The user to retrieve the password for. </param>
        /// <param name="answer">The password answer for the user. </param>
        /// <returns>
        /// The password for the specified user name.
        /// </returns>
        string GetPassword(string username, string answer);

        /// <summary>
        /// When overridden in a derived class, returns the date and time when the password was most recently changed for the specified membership account.
        /// </summary>
        /// <param name="userName">The user name of the account.</param>
        /// <returns>
        /// The date and time when the password was more recently changed for membership account, or <see cref="F:System.DateTime.MinValue"/> if the password has never been changed for this user account.
        /// </returns>
        DateTime GetPasswordChangedDate(string userName);

        /// <summary>
        /// When overridden in a derived class, returns the number of times that the password for the specified user account was incorrectly entered since the most recent successful login or since the user account was created.
        /// </summary>
        /// <param name="userName">The user name of the account.</param>
        /// <returns>
        /// The count of failed password attempts for the specified user account.
        /// </returns>
        int GetPasswordFailuresSinceLastSuccess(string userName);

        /// <summary>
        /// Gets user information from the data source based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Registration.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        MembershipUser GetUser(object providerUserKey, bool userIsOnline);

        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="username">The name of the user to get information for. </param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user. </param>
        /// <returns>
        /// A <see cref="T:System.Web.Registration.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        MembershipUser GetUser(string username, bool userIsOnline);

        /// <summary>
        /// When overridden in a derived class, returns an ID for a user based on a password reset token.
        /// </summary>
        /// <param name="token">The password reset token.</param>
        /// <returns>
        /// The user ID.
        /// </returns>
        int GetUserIdFromPasswordResetToken(string token);

        /// <summary>
        /// Gets the user name associated with the specified e-mail address.
        /// </summary>
        /// <param name="email">The e-mail address to search for. </param>
        /// <returns>
        /// The user name associated with the specified e-mail address. If no match is found, return null.
        /// </returns>
        string GetUserNameByEmail(string email);

        /// <summary>
        /// When overridden in a derived class, returns a value that indicates whether the user account has been confirmed by the provider.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>
        /// true if the user is confirmed; otherwise, false.
        /// </returns>
        bool IsConfirmed(string userName);

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// <param name="username">The user to reset the password for. </param>
        /// <param name="answer">The password answer for the specified user. </param>
        /// <returns>
        /// The new password for the specified user.
        /// </returns>
        string ResetPassword(string username, string answer);

        /// <summary>
        /// When overridden in a derived class, resets a password after verifying that the specified password reset token is valid.
        /// </summary>
        /// <param name="token">A password reset token.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>
        /// true if the password was changed; otherwise, false.
        /// </returns>
        bool ResetPasswordWithToken(string token, string newPassword);

        /// <summary>
        /// Clears a lock so that the membership user can be validated.
        /// </summary>
        /// <param name="userName">The membership user whose lock status you want to clear.</param>
        /// <returns>
        /// true if the membership user was successfully unlocked; otherwise, false.
        /// </returns>
        bool UnlockUser(string userName);

        /// <summary>
        /// Updates information about a user in the data source.
        /// </summary>
        /// <param name="user">A <see cref="T:System.Web.Registration.MembershipUser"/> object that represents the user to update and the updated information for the user. </param>
        void UpdateUser(MembershipUser user);

        /// <summary>
        /// Verifies that the specified user name and password exist in the data source.
        /// </summary>
        /// <param name="username">The name of the user to validate. </param>
        /// <param name="password">The password for the specified user. </param>
        /// <returns>
        /// true if the specified username and password are valid; otherwise, false.
        /// </returns>
        bool ValidateUser(string username, string password);
    }
}
