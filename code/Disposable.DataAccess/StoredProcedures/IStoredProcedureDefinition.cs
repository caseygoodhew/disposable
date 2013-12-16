using System.Collections.Generic;

namespace Disposable.DataAccess.StoredProcedures
{
    /// <summary>
    /// Defines the required attributes to call a stored procedure
    /// </summary>
    public interface IStoredProcedureDefinition
    {
        /// <summary>
        /// Gets the package name where the procedure resides
        /// </summary>
        string Package { get; }

        /// <summary>
        /// Gets the name of the procedure
        /// </summary>
        string Procedure { get; }

        /// <summary>
        /// Gets the input parameters expected by the procedure
        /// </summary>
        IEnumerable<InputParameter> InputParameters { get; }

        /// <summary>
        /// Gets the output parameter provided by the procedure
        /// </summary>
        OutputParameter OutputParameter { get; }
    }
}
