using System.Data;

namespace Disposable.Data.Packages.Core
{
    public class InputParameter : Parameter, IInputParameter
    {
        public bool Required { get; private set; }

        public InputParameter(string name, DataTypes dataType, bool required = true) : base(name, dataType)
        {
            Required = required;
        }
    }
}
