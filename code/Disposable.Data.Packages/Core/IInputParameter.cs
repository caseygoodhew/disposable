namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Interface for a stored method input parameter.
    /// </summary>
    public interface IInputParameter : IParameter
    {
        /// <summary>
        /// Gets a value indicating whether if the parameter is required.
        /// </summary>
        bool Required { get; }
    }
}
