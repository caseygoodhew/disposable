using MySql.Data.MySqlClient;

namespace Disposable.DataAccess.StoredProcedures
{
    public class OutputParameter : Parameter
    {
        public OutputParameter(string name, MySqlDbType dataType) : base(name, dataType)
        {
        }
    }
}
