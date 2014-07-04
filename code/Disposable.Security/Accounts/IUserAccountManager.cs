using System.Collections.Generic;

namespace Disposable.Security.Accounts
{
    /// <summary>
    /// Interface for implementations which provide user account management services.
    /// </summary>
    public interface IUserAccountManager
    {
        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        /// <remarks>This is my own addition.</remarks>
        bool ApproveUser();

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        /// <remarks>system arguments: string username, string oldPassword, string newPassword</remarks>
        bool ChangePassword();

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password (empty passwords will be set to a securely random value)</param>
        /// <param name="isApproved">Sets the initial approval state for the user.</param>
        /// <param name="status">The resultant status of the account creation.</param>
        /// <param name="confirmationCode">A confirmation code that can be used to validate a user's email address.</param>
        /// <returns>The user id.</returns>
        /// <remarks>system arguments: string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status</remarks>
        long CreateUser(string email, string password, bool isApproved, out UserAccountCreateStatus status, out string confirmationCode);

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        bool DeleteUser();

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        IList<IUser> FindUsers();

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        IList<IUser> GetAllUsers();

        /// <summary>
        /// Gets the user account by username. 
        /// </summary>
        /// <param name="username">The username (email address) of the user.</param>
        /// <returns>The user's details</returns>
        /// <remarks>TODO: Deprecate</remarks>
        /// <remarks>system arguments: object providerUserKey, bool userIsOnline</remarks>
        IUser GetUser(string username);

        /// <summary>
        /// Determines if the user account exists locally.
        /// </summary>
        /// <param name="userId">The used id to check.</param>
        /// <returns>true if the user account exists locally.</returns>
        bool HasLocalAccount(long userId);

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        /// <remarks>This is my own addition.</remarks>
        bool LockUser();

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        /// <returns>The parameter is not used.</returns>
        /// <remarks>system arguments: string userName</remarks>
        bool UnlockUser();

        /// <summary>
        /// The parameter is not used.
        /// </summary>
        void UpdateUser();
    }
}
