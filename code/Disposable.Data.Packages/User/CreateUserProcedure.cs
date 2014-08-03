using System;
using System.Collections.Generic;
using System.Data;
using Disposable.Data.Common.Exceptions;
using Disposable.Data.Packages.Core;
using Disposable.Data.Security.Accounts.Exceptions;

namespace Disposable.Data.Packages.User
{
    /// <summary>
    /// Creates a new user account.
    /// </summary>
    internal class CreateUserProcedure : StoredProcedure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserProcedure"/> class.
        /// </summary>
        /// <param name="package">The <see cref="IPackage"/> to which the procedure belongs.</param>
        public CreateUserProcedure(IPackage package) 
            : base(
                package, 
                PackageConstants.CreateUser, 
                new InputParameter(PackageConstants.InEmail, DataTypes.String),
                new InputParameter(PackageConstants.InPassword, DataTypes.String),
                new InputParameter(PackageConstants.InApproved, DataTypes.Boolean),
                new OutputParameter(PackageConstants.OutUserSid, DataTypes.Long),
                new OutputParameter(PackageConstants.OutConfirmationGuid, DataTypes.Guid))
        {
        }

        /// <summary>
        /// Instructs an <see cref="IStoredMethod"/> to handle a <see cref="ProgrammaticDatabaseException"/> which 
        /// was thrown in the database when the <see cref="IStoredMethodInstance"/> was invoked.
        /// </summary>
        /// <param name="storedMethodInstance">The <see cref="IStoredMethodInstance"/> that was running at the time of error generation.</param>
        /// <param name="exceptionDescription">The <see cref="ExceptionDescription"/> of the error to handle.</param>
        /// <param name="underlyingDatabaseException">The <see cref="UnderlyingDatabaseException"/></param>
        /// <returns>
        /// If the exception has been handled, this should be the same value that was passed into the method. 
        /// Returning any value other than this will result in an <see cref="UnhandledDatabaseException"/> being immediately thrown.
        /// </returns>
        public override ExceptionDescription Handle(IStoredMethodInstance storedMethodInstance, ExceptionDescription exceptionDescription, UnderlyingDatabaseException underlyingDatabaseException)
        {
            if (exceptionDescription == ProgrammaticDatabaseExceptions.DuplicateEmail)
            {
                var email = storedMethodInstance.GetValue<IInputParameterValue>(PackageConstants.InEmail).ToString();
                throw new DuplicateEmailException(email);
            }

            return base.Handle(storedMethodInstance, exceptionDescription, underlyingDatabaseException);
        }

        /// <summary>
        /// Sets the parameters for the stored procedure arguments
        /// </summary>
        /// <param name="email">The email to authenticate</param>
        /// <param name="password">The password to authenticate</param>
        /// <param name="isApproved">Flag indicating if the user should be created in an approved state</param>
        internal IStoredMethodInstance CreateInstance(string email, string password, bool isApproved)
        {
            var instance = CreateInstance();

            instance.SetValues<IInputParameterValue>(new Dictionary<string, object>
            {
                { PackageConstants.InEmail, email },
                { PackageConstants.InPassword, password },
                { PackageConstants.InApproved, isApproved }
            });

            return instance;
        }
    }
}
