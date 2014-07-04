namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Interface for a stored method parameter.
    /// </summary>
    public interface IParameter
    {
        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the data type of the parameter.
        /// </summary>
        DataTypes DataType { get; }
    }
}
