namespace Disposable.Data.Packages.Core
{
    public class OutputParameterValue : ParameterValue<IOutputParameter>, IOutputParameter
    {
        public OutputParameterValue(IOutputParameter parameter, object value = null) : base(parameter, value)
        {
        }
    }
}
