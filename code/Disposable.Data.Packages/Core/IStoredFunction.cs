using System.Collections.Generic;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Defines the required attributes to call a stored function.
    /// </summary>
    public interface IStoredFunction : IStoredMethod
    {
       /// <summary>
        /// Gets the output parameter value from the stored function.
        /// </summary>
        /// <returns>The <see cref="OutputParameterValue"/></returns>
        OutputParameterValue GetOutputParameterValue();
    }
}
