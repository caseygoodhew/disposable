namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Abstract implementation for a stored method parameter.
    /// </summary>
    public abstract class Parameter : IParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter" /> class.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="dataType">The parameter data type.</param>
        protected Parameter(string name, DataTypes dataType)
        {
            Name = name;
            DataType = dataType;
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the data type of the parameter.
        /// </summary>
        public DataTypes DataType { get; private set; }
    }
}
