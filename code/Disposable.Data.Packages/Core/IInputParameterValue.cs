namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Interface for a stored method input parameter value.
    /// </summary>
    public interface IInputParameterValue : IParameterValue
    {
        /// <summary>
        /// Gets a value indicating whether if the parameter is required.
        /// </summary>
        bool Required { get; }
    }
}
