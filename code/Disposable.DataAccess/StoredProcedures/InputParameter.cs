using MySql.Data.MySqlClient;

namespace Disposable.DataAccess.StoredProcedures
{
    public class InputParameter : Parameter
    {
        internal readonly bool Required;

        public InputParameter(string name, MySqlDbType dataType, bool required = true) : base(name, dataType)
        {
            Required = required;
        }
    }
}
