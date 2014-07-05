using System.Collections.Generic;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Interface defining the required attributes to call a stored procedure.
    /// </summary>
    public interface IStoredProcedure : IStoredMethod
    {
        /// <summary>
        /// Gets the output parameter values that will be used to call the stored procedure.
        /// </summary>
        /// <returns>A list of the output parameters.</returns>
        IList<OutputParameterValue> GetOutputParameters();
    }
}
