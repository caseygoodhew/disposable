namespace Disposable.Data.Packages.Core
{
    public abstract class ParameterValue<T> : IParameter where T : IParameter
    {
        protected readonly T Parameter;

        public string Name
        {
            get
            {
                return Parameter.Name;
            }
        }

        public DataTypes DataType
        {
            get
            {
                return Parameter.DataType;
            }
        }
        
        public object Value { get; set; }

        protected ParameterValue(T parameter, object value = null)
        {
            Parameter = parameter;
            Value = value;
        }
    }
}
