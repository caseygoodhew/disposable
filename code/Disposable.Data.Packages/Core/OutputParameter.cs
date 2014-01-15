using System.Data;

namespace Disposable.Data.Packages.Core
{
    public class OutputParameter : Parameter, IOutputParameter
    {
        public OutputParameter(string name, DataTypes dataType) : base(name, dataType)
        {
        }
    }
}
