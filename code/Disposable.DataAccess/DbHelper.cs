using System;
using System.Collections.Generic;
using System.Data;
using Disposable.Common.ServiceLocator;
using Disposable.DataAccess.Database;
using Disposable.DataAccess.Database.Oracle;
using Disposable.DataAccess.Packages.Core;
using IDbConnection = Disposable.DataAccess.Database.IDbConnection;

namespace Disposable.DataAccess
{
    /// <summary>
    /// Wrapper to manage a <see cref="IDbConnection"/>
    /// </summary>
    public class DbHelper : IDbHelper
    {
        private readonly static Lazy<IConnectionProvider> WebConnectionProvider = new Lazy<IConnectionProvider>(
            () => Locator.Current.Instance<IConnectionProvider>());
        
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
                    _connection = WebConnectionProvider.Value.CreateConnection(ConnectionType.Web, null, Guid.Empty);
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
        /*public bool ReturnBool<T>(IDictionary<string, object> parameters) where T : IStoredProcedure, new()
        {
            
            
            //var storedProcedure = StoredProcedure.Create<T>(parameters);
            /*
            var cmd = new MySqlCommand(storedProcedure.CommandText(), Connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            storedProcedure.InputParameters().ToList().ForEach(x => cmd.Parameters.Add(x));

            var outputParam = cmd.Parameters.Add(storedProcedure.OutputParameter());
            
            cmd.ExecuteNonQuery();

            return Convert.ToBoolean(outputParam.Value);* /
            return true;
        }*/

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

        public bool ReturnBool(IStoredProcedure storedProcedure)
        {
            throw new System.NotImplementedException();
        }
    }
}
