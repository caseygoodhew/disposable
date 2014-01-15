using System.Collections.Generic;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Defines the required attributes to call a stored function
    /// </summary>
    public interface IStoredFunction : IStoredMethod
    {
       /// <summary>
        /// Gets the output parameter that will be used to call the stored function
        /// </summary>
        /// <returns></returns>
        OutputParameterValue GetOutputParameter();
    }
}
