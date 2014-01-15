namespace Disposable.Data.Packages.Core
{
    public class InputParameterValue : ParameterValue<IInputParameter>, IInputParameter
    {
        public bool Required
        {
            get
            {
                return Parameter.Required;
            }
        }

        public InputParameterValue(IInputParameter parameter, object value = null) : base(parameter, value)
        {
        }
    }
}
