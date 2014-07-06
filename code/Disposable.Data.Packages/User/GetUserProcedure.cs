using System;
using System.Collections.Generic;
using Disposable.Data.Packages.Core;

namespace Disposable.Data.Packages.User
{
    /// <summary>
    /// Gets a user.
    /// </summary>
    internal class GetUserProcedure : StoredProcedure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserProcedure"/> class.
        /// </summary>
        /// <param name="package">The <see cref="IPackage"/> to which the procedure belongs.</param>
        public GetUserProcedure(IPackage package) 
            : base(
                package, 
                PackageConstants.CreateUser, 
                new InputParameter(PackageConstants.InUsername, DataTypes.String),
                new OutputParameter(PackageConstants.OutCursor, DataTypes.Cursor))
        {
        }

        /// <summary>
        /// Sets the parameters for the stored procedure arguments
        /// </summary>
        /// <param name="username">The username of the user to get</param>
        [Obsolete("Will be removed once no longer used in MVC.")]
        internal void SetParameterValues(string username)
        {
            this.SetInputParameterValues(new Dictionary<string, object>
            {
                { PackageConstants.InUsername, username }
            });
        }
    }
}
