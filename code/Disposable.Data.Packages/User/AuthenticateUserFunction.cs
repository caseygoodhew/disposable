using System.Collections.Generic;
using System.Data;
using Disposable.Data.Packages.Core;

namespace Disposable.Data.Packages.User
{
    internal class AuthenticateUserFunction : StoredFunction
    {
        public AuthenticateUserFunction(IPackage package) 
            : base(
                package, 
                PackageConstants.Authenticate, 
                new InputParameter(PackageConstants.InUsername, DataTypes.String),
                new InputParameter(PackageConstants.InPassword, DataTypes.String),
                new OutputParameter(PackageConstants.OutResult, DataTypes.Boolean) 
            )
        {
        }

        /// <summary>
        /// Sets the parameters for the stored procedure arguments
        /// </summary>
        /// <param name="username">The username to authenticate</param>
        /// <param name="password">The password to authenticate</param>
        internal void SetParameterValues(string username, string password)
        {
            SetParameterValues(new Dictionary<string, object>
            {
                { PackageConstants.InUsername, username },
                { PackageConstants.InPassword, password }
            });
        }
    }
}
