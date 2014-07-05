using System;
using System.Collections.Generic;
using System.Data;
using Disposable.Data.Common.Exceptions;
using Disposable.Data.Packages.Core;
using Disposable.Data.Security.Accounts.Exceptions;

namespace Disposable.Data.Packages.User
{
    internal class CreateUserProcedure : StoredProcedure
    {
        public CreateUserProcedure(IPackage package) 
            : base(
                package, 
                PackageConstants.CreateUser, 
                new InputParameter(PackageConstants.InEmail, DataTypes.String),
                new InputParameter(PackageConstants.InPassword, DataTypes.String),
                new InputParameter(PackageConstants.InApproved, DataTypes.Boolean),
                new OutputParameter(PackageConstants.OutUserSid, DataTypes.Long),
                new OutputParameter(PackageConstants.OutConfirmationGuid, DataTypes.Guid)
            )
        {
        }

        /// <summary>
        /// Sets the parameters for the stored procedure arguments
        /// </summary>
        /// <param name="email">The email to authenticate</param>
        /// <param name="password">The password to authenticate</param>
        /// <param name="isApproved">Flag indicating if the user should be created in an approved state</param>
        internal void SetParameterValues(string email, string password, bool isApproved)
        {
            this.SetInputParameterValues(new Dictionary<string, object>
            {
                { PackageConstants.InEmail, email },
                { PackageConstants.InPassword, password },
                { PackageConstants.InApproved, isApproved }
            });
        }

        public override ProgrammaticDatabaseExceptions Handle(ProgrammaticDatabaseExceptions programmaticDatabaseException)
        {
            if (programmaticDatabaseException == ProgrammaticDatabaseExceptions.DuplicateEmail)
            {
                throw new DuplicateEmailException(GetInputParameterValue(PackageConstants.InEmail).Value.ToString());
            }

            return base.Handle(programmaticDatabaseException);
        }
    }
}
