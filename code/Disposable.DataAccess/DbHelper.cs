using System.Collections;
using System.Collections.Generic;
using Dapper;
using System;
using System.Data;
using System.Linq;
using Disposable.DataAccess.StoredProcedures;

namespace Disposable.DataAccess
{
	/// <summary>
	/// 
	/// </summary>
    public class DbHelper : IDbHelper
	{
        // ReSharper disable once ConvertToConstant.Local
	    //private static readonly string ConnectionString = "Server=localhost;Database=disposable;Uid=root;Pwd=manager;";

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
	                //_connection = new MySqlConnection(ConnectionString);
                    _connection.Open();
	            }

	            return _connection;
	        }
	    }
        
        private string TestDirect()
		{
            /*var db = new MySqlConnection(ConnectionString);
			db.Open();

			var cmd = new MySqlCommand("Select * from disposable.class_type where class_type_id = 1", db);
			IDataReader rdr = cmd.ExecuteReader();
			
			rdr.Read();
			string value = rdr[rdr.GetOrdinal("description")].ToString();
			
			db.Close();
			
			return value;*/
            return string.Empty;
		}

        private string TestDapper()
		{
			/*var db = new MySqlConnection("Server=localhost;Database=disposable;Uid=root;Pwd=manager;");
			
            db.Open();
            
			
            return
                db.Query<DbHelper>("select * from disposable.class_type where class_type_id = @ClassTypeId", new { ClassTypeId = 1 })
					.First()
					.ToString();*/

            return string.Empty;
		}
        
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
