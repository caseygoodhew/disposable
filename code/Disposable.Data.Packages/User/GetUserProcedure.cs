using System;
using System.Collections.Generic;
using Disposable.Data.Common.Exceptions;
using Disposable.Data.Packages.Core;

namespace Disposable.Data.Packages.User
{
    internal class GetUserProcedure : StoredProcedure
    {
        public GetUserProcedure(IPackage package) 
            : base(
                package, 
                PackageConstants.CreateUser, 
                new InputParameter(PackageConstants.InUsername, DataTypes.String),
                new OutputParameter(PackageConstants.OutCursor, DataTypes.Cursor)
            )
        {
        }

        /// <summary>
        /// Sets the parameters for the stored procedure arguments
        /// </summary>
        /// <param name="username">The username of the user to get</param>
        internal void SetParameterValues(string username)
        {
            SetParameterValues(new Dictionary<string, object>
            {
                { PackageConstants.InUsername, username }
            });
        }

        public override void Throw(ProgrammaticDatabaseExceptions programmaticDatabaseException, Exception exception)
        {
        }
    }
}
