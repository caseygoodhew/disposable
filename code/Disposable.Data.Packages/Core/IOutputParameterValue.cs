using System.Data;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Interface for a stored method output parameter.
    /// </summary>
    public interface IOutputParameterValue : IParameterValue
    {
        /// <summary>
        /// Gets the output parameter value as an <see cref="OutputParameter"/>.
        /// </summary>
        /// <returns></returns>
        IOutputParameter AsOutputParameter();
    }
}
