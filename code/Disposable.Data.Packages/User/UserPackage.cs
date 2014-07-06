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
        public UserPackage()
        {
            Register(() => new AuthenticateUserProcedure(this));
            Register(() => new CreateUserProcedure(this));
            Register(() => new GetUserProcedure(this));
        }

        /// <summary>
        /// Gets the package schema name.
        /// </summary>
        public override string Schema
        {
            get { return PackageConstants.Disposable; } 
        }

        /// <summary>
        /// Gets the package name.
        /// </summary>
        public override string Name
        {
            get { return PackageConstants.UserPkg; } 
        }

        /// <summary>
        /// Authenticates a user by their email address and password.
        /// </summary>
        /// <param name="email">The username to authenticate</param>
        /// <param name="password">The password to authenticate</param>
        /// <returns>The <see cref="IStoredProcedure"/>.</returns>
        public IStoredProcedure AuthenticateUserProcedure(string email, string password)
        {
            var procedure = Instance<AuthenticateUserProcedure>();
            procedure.SetParameterValues(email, password);
            return procedure;
        }

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="email">The email to authenticate</param>
        /// <param name="password">The password to authenticate</param>
        /// <param name="isApproved">Flag indicating if the user should be created in an approved state</param>
        /// <returns>The <see cref="IStoredProcedure"/>.</returns>
        public IStoredProcedure CreateUserProcedure(string email, string password, bool isApproved)
        {
            var procedure = Instance<CreateUserProcedure>();
            procedure.SetParameterValues(email, password, isApproved);
            return procedure;
        }

        /// <summary>
        /// Gets a user.
        /// </summary>
        /// <param name="username">The username of the user to get</param>
        /// <returns>The <see cref="IStoredProcedure"/>.</returns>
        [Obsolete("Will be removed once no longer used in MVC.")]
        public IStoredProcedure GetUserProcedure(string username)
        {
            var procedure = Instance<GetUserProcedure>();
            procedure.SetParameterValues(username);
            return procedure;
        }
    }
}
