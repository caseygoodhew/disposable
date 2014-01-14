using System.Data;

namespace Disposable.DataAccess.Packages.Core
{
    public class OutputParameter : Parameter
    {
        public OutputParameter(string name, DbType dataType) : base(name, dataType)
        {
        }
    }
}
