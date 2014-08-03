using System;
using Disposable.Data.Packages.Core;

namespace Disposable.Data.Packages.User
{
    /// <summary>
    /// Procedures that are contained in the user package.
    /// </summary>
    public class UserPackage : Package, IUserPackage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPackage"/> class.
        /// </summary>
        public UserPackage() : base(PackageConstants.Disposable, PackageConstants.UserPkg)
        {
            Add(() => new AuthenticateUserProcedure(this));
            Add(() => new CreateUserProcedure(this));
            Add(() => new GetUserProcedure(this));
        }

        /// <summary>
        /// Authenticates a user by their email address and password.
        /// </summary>
        /// <param name="email">The username to authenticate</param>
        /// <param name="password">The password to authenticate</param>
        /// <returns>The <see cref="IStoredProcedure"/>.</returns>
        public IStoredMethodInstance AuthenticateUserProcedure(string email, string password)
        {
            return Get<AuthenticateUserProcedure>().CreateInstance(email, password);
        }

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="email">The email to authenticate</param>
        /// <param name="password">The password to authenticate</param>
        /// <param name="isApproved">Flag indicating if the user should be created in an approved state</param>
        /// <returns>The <see cref="IStoredProcedure"/>.</returns>
        public IStoredMethodInstance CreateUserProcedure(string email, string password, bool isApproved)
        {
            return Get<CreateUserProcedure>().CreateInstance(email, password, isApproved);
        }

        /// <summary>
        /// Gets a user.
        /// </summary>
        /// <param name="username">The username of the user to get</param>
        /// <returns>The <see cref="IStoredProcedure"/>.</returns>
        [Obsolete("Will be removed once no longer used in MVC.")]
        public IStoredMethodInstance GetUserProcedure(string username)
        {
            return Get<GetUserProcedure>().CreateInstance(username);
        }
    }
}
