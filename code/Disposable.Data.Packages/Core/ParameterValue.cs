namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Abstract implementation for a stored method parameter value.
    /// </summary>
    /// <typeparam name="T">The <see cref="IParameter"/> that the value corresponds to.</typeparam>
    public abstract class ParameterValue<T> : IParameterValue where T : IParameter
    {
        /// <summary>
        /// The underlying <see cref="IParameter"/>.
        /// </summary>
        protected readonly T Parameter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterValue{T}"/> class.
        /// </summary>
        /// <param name="parameter">The underlying <see cref="IParameter"/>.</param>
        /// <param name="value">(Optional) The initial value.</param>
        protected ParameterValue(T parameter, object value = null)
        {
            Parameter = parameter;
            Value = value;
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Name
        {
            get
            {
                return Parameter.Name;
            }
        }

        /// <summary>
        /// Gets the data type of the parameter.
        /// </summary>
        public DataTypes DataType
        {
            get
            {
                return Parameter.DataType;
            }
        }

        /// <summary>
        /// Gets or sets the parameter value.
        /// </summary>
        public object Value { get; set; }
    }
}
