using System;
using System.Collections.Generic;
using Disposable.Data.Common.Exceptions;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Interface defining the common attributes required to call a stored method.
    /// </summary>
    public interface IStoredMethod
    {
        /// <summary>
        /// Gets the <see cref="IPackage"/> which owns the <see cref="IStoredProcedure"/>
        /// </summary>
        IPackage Package { get; }
        
        /// <summary>
        /// Gets the name of the procedure
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the input parameter values.
        /// </summary>
        /// <param name="includeEmpty">Flag to include empty parameters.</param>
        /// <returns>A list of <see cref="InputParameterValue"/></returns>
        IList<InputParameterValue> GetInputParameterValues(bool includeEmpty);

        /// <summary>
        /// Gets an input parameter value.
        /// </summary>
        /// <param name="name">The name of the parameter to get.</param>
        /// <returns>The <see cref="InputParameterValue"/></returns>
        InputParameterValue GetInputParameterValue(string name);

        /// <summary>
        /// Instructs an <see cref="IStoredMethod"/> to handle a <see cref="ProgrammaticDatabaseException"/> which 
        /// was thrown in the database when the <see cref="IStoredMethod"/> was invoked.
        /// </summary>
        /// <param name="exceptionDescription">The <see cref="ProgrammaticDatabaseException"/> name.</param>
        /// <param name="underlyingDatabaseException">The <see cref="UnderlyingDatabaseException"/></param>
        /// <returns>
        /// If the exception has been handled, this should be the same value that was passed into the method. 
        /// Returning any value other than this will result in an <see cref="UnhandledDatabaseException"/> being immediately thrown.
        /// </returns>
        ExceptionDescription Handle(
            ExceptionDescription exceptionDescription,
            UnderlyingDatabaseException underlyingDatabaseException);
    }
}
