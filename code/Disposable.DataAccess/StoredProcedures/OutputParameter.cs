using System.Data;

namespace Disposable.DataAccess.StoredProcedures
{
    public class OutputParameter : Parameter
    {
        public OutputParameter(string name, DbType dataType) : base(name, dataType)
        {
        }
    }
}
