namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Interface for a stored method parameter value.
    /// </summary>
    public interface IParameterValue
    {
        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the data type of the parameter.
        /// </summary>
        DataTypes DataType { get; }

        /// <summary>
        /// Gets or sets the parameter value.
        /// </summary>
        object Value { get; set; }
    }
}
