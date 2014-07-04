namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Implementation for a stored method input parameter.
    /// </summary>
    public class InputParameter : Parameter, IInputParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputParameter" /> class.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="dataType">The parameter data type.</param>
        /// <param name="required">Flag that the parameter is required.</param>
        public InputParameter(string name, DataTypes dataType, bool required = true) : base(name, dataType)
        {
            Required = required;
        }

        /// <summary>
        /// Gets a value indicating whether if the parameter is required.
        /// </summary>
        public bool Required { get; private set; }
    }
}
