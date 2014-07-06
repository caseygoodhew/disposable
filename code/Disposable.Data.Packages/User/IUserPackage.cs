using System;

using Disposable.Data.Packages.Core;

namespace Disposable.Data.Packages.User
{
    /// <summary>
    /// Interface defining procedures that are contained in the user package.
    /// </summary>
    public interface IUserPackage : IPackage
    {
        /// <summary>
        /// Authenticates a user by their email address and password.
        /// </summary>
        /// <param name="email">The username to authenticate</param>
        /// <param name="password">The password to authenticate</param>
        /// <returns>The <see cref="IStoredProcedure"/>.</returns>
        IStoredProcedure AuthenticateUserProcedure(string email, string password);

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="email">The email to authenticate</param>
        /// <param name="password">The password to authenticate</param>
        /// <param name="isApproved">Flag indicating if the user should be created in an approved state</param>
        /// <returns>The <see cref="IStoredProcedure"/>.</returns>
        IStoredProcedure CreateUserProcedure(string email, string password, bool isApproved);

        /// <summary>
        /// Gets a user.
        /// </summary>
        /// <param name="username">The username of the user to get</param>
        /// <returns>The <see cref="IStoredProcedure"/>.</returns>
        [Obsolete("Will be removed once no longer used in MVC.")]
        IStoredProcedure GetUserProcedure(string username);
    }
}
