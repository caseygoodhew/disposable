namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Stored method output parameter.
    /// </summary>
    public class OutputParameter : Parameter, IOutputParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputParameter"/> class.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="dataType">The parameter data type.</param>
        public OutputParameter(string name, DataTypes dataType) : base(name, dataType)
        {
        }
    }
}
