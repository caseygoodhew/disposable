namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// <see cref="OutputParameter"/> value.
    /// </summary>
    public class OutputParameterValue : ParameterValue<IOutputParameter>, IOutputParameterValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputParameterValue"/> class.
        /// </summary>
        /// <param name="parameter">The underlying <see cref="IOutputParameter"/>.</param>
        /// <param name="value">(Optional) The initial value.</param>
        public OutputParameterValue(IOutputParameter parameter, object value = null) : base(parameter, value)
        {
        }

        /// <summary>
        /// Gets the output parameter value as an <see cref="OutputParameter"/>.
        /// </summary>
        /// <returns></returns>
        public IOutputParameter AsOutputParameter()
        {
            return Parameter;
        }
    }
}
