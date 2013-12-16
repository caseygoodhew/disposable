using MySql.Data.MySqlClient;

namespace Disposable.DataAccess.StoredProcedures
{
    public abstract class Parameter
    {
        internal readonly string Name;

        internal readonly MySqlDbType DataType;

        protected Parameter(string name, MySqlDbType dataType)
        {
            Name = name;
            DataType = dataType;
        }
    }
}
