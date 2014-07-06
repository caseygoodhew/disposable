using System.Collections.Generic;
using System.Data;
using Disposable.Data.Packages.Core;

namespace Disposable.Data.Packages.User
{
    /// <summary>
    /// Authenticates a user by their email address and password.
    /// </summary>
    internal class AuthenticateUserProcedure : StoredProcedure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateUserProcedure"/> class.
        /// </summary>
        /// <param name="package">The <see cref="IPackage"/> to which the procedure belongs.</param>
        public AuthenticateUserProcedure(IPackage package) 
            : base(
                package, 
                PackageConstants.Authenticate, 
                new InputParameter(PackageConstants.InEmail, DataTypes.String),
                new InputParameter(PackageConstants.InPassword, DataTypes.String),
                new OutputParameter(PackageConstants.OutResult, DataTypes.Boolean))
        {
        }

        /// <summary>
        /// Sets the parameters for the stored procedure arguments
        /// </summary>
        /// <param name="email">The username to authenticate</param>
        /// <param name="password">The password to authenticate</param>
        internal void SetParameterValues(string email, string password)
        {
            this.SetInputParameterValues(new Dictionary<string, object>
            {
                { PackageConstants.InEmail, email },
                { PackageConstants.InPassword, password }
            });
        }
    }
}
