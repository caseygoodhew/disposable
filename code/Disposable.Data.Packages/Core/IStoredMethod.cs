using System;
using System.Collections.Generic;
using Disposable.Data.Common.Exceptions;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Defines the required attributes to call a stored method
    /// </summary>
    public interface IStoredMethod
    {
        /// <summary>
        /// Get the <see cref="IPackage"/> which owns the <see cref="IStoredProcedure"/>
        /// </summary>
        IPackage Package { get; }
        
        /// <summary>
        /// Gets the name of the procedure
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the input parameter values that will be used to call the stored method
        /// </summary>
        /// <param name="includeEmpty"></param>
        /// <returns></returns>
        IList<InputParameterValue> GetInputParameters(bool includeEmpty);

        InputParameterValue GetInputParameterValue(string name);

        /// <summary>
        /// Instructs an <see cref="IStoredMethod"/> to handle a <see cref="ProgrammaticDatabaseException"/> which 
        /// was thrown in the database when the <see cref="IStoredMethod"/> was invoked.
        /// </summary>
        /// <param name="programmaticDatabaseException">The normalized <see cref="ProgrammaticDatabaseException"/>.</param>
        /// <returns>
        /// If the exception has been handled, this should be the same value that was passed into the method. 
        /// Returning any value other than this will result in an <see cref="UnhandledDatabaseException"/> being immediately thrown.
        /// </returns>
        ProgrammaticDatabaseExceptions Handle(ProgrammaticDatabaseExceptions programmaticDatabaseException);
    }
}
