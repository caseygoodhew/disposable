using Disposable.DataAccess.StoredProcedures;
using System.Collections.Generic;
using System.Data;

namespace Disposable.DataAccess
{
    /// <summary>
    /// Wrapper to manage a <see cref="DbConnection"/>
    /// </summary>
    public class DbHelper : IDbHelper
    {
        private static readonly string WebConnectionString = "Password=upd;Persist Security Info=True;User ID=upd;Data Source=disposable;";

        private IDbConnection _connection;

        private IDbConnection Connection
        {
            get
            {
                if (_connection != null && _connection.State == ConnectionState.Closed)
                {
                    _connection.Dispose();
                    _connection = null;
                }
                
                if (_connection == null)
                {
                    _connection = new DbConnection(WebConnectionString);
                    _connection.Open();
                }

                return _connection;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool ReturnBool<T>(IDictionary<string, object> parameters) where T : IStoredProcedureDefinition, new()
        {
            var storedProcedure = StoredProcedure.Create<T>(parameters);
            /*
            var cmd = new MySqlCommand(storedProcedure.CommandText(), Connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            storedProcedure.InputParameters().ToList().ForEach(x => cmd.Parameters.Add(x));

            var outputParam = cmd.Parameters.Add(storedProcedure.OutputParameter());
            
            cmd.ExecuteNonQuery();

            return Convert.ToBoolean(outputParam.Value);*/
            return true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    // see http://dev.mysql.com/doc/refman/5.5/en/server-system-variables.html#sysvar_interactive_timeout
                    _connection.Close();
                }

                _connection.Dispose();
            }
        }
    }
}
