namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// <see cref="InputParameterValue"/> value.
    /// </summary>
    public class InputParameterValue : ParameterValue<IInputParameter>, IInputParameterValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputParameterValue" /> class.
        /// </summary>
        /// <param name="parameter">The underlying <see cref="IInputParameter"/>.</param>
        /// <param name="value">(Optional) The initial value.</param>
        public InputParameterValue(IInputParameter parameter, object value = null) : base(parameter, value)
        {
        }
        
        /// <summary>
        /// Gets a value indicating whether if the parameter is required.
        /// </summary>
        public bool Required
        {
            get
            {
                return Parameter.Required;
            }
        }

        /// <summary>
        /// Gets the input parameter value as an <see cref="InputParameter"/>.
        /// </summary>
        /// <returns></returns>
        public IInputParameter AsInputParameter()
        {
            return Parameter;
        }
    }
}
