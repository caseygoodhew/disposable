using System.Collections.Generic;
using System.Data;
using Disposable.DataAccess.Packages.Core;

namespace Disposable.DataAccess.Packages.User
{
    internal class AuthenticateUserProcedure : StoredProcedure
    {
        public AuthenticateUserProcedure(IPackage package) 
            : base(
                package, 
                PackageConstants.Authenticate, 
                new OutputParameter(PackageConstants.OutResult, DbType.Boolean),
                new InputParameter(PackageConstants.InUsername, DbType.String),
                new InputParameter(PackageConstants.InPassword, DbType.String)
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
                {PackageConstants.InUsername, username},
                {PackageConstants.InPassword, password}
            });
        }
    }
}
