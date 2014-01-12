using System.Collections.Generic;

namespace Disposable.Packages.Core
{
    /// <summary>
    /// Defines the required attributes to call a stored procedure
    /// </summary>
    internal interface IStoredProcedureDefinition
    {
        IPackage Package { get; }
        
        /// <summary>
        /// Gets the name of the procedure
        /// </summary>
        string Procedure { get; }

        /// <summary>
        /// Gets the input parameters expected by the procedure
        /// </summary>
        IList<InputParameter> InputParameters { get; }

        /// <summary>
        /// Gets the output parameter provided by the procedure
        /// </summary>
        OutputParameter OutputParameter { get; }

        /// <summary>
        /// Gets the parameters that will be used to call the stored procedure
        /// </summary>
        /// <returns></returns>
        IDictionary<string, object> GetParameters();
    }
}
