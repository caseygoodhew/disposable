namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Implementation for a stored method input parameter value.
    /// </summary>
    public class InputParameterValue : ParameterValue<IInputParameter>, IInputParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputParameterValue" /> class.
        /// </summary>
        /// <param name="parameter">The input parameter.</param>
        /// <param name="value">The parameter value.</param>
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
    }
}
