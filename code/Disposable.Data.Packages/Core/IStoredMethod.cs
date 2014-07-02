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

        void Throw(ProgrammaticDatabaseExceptions programmaticDatabaseException, Exception exception);
    }
}
