using System;
using System.Collections.Generic;
using System.Data;
using Disposable.Common.ServiceLocator;
using Disposable.Data.Access.Database;
using Disposable.Data.Access.Database.Oracle;
using Disposable.Data.Packages.Core;
using IDbConnection = Disposable.Data.Access.Database.IDbConnection;

namespace Disposable.Data.Access
{
    /// <summary>
    /// Wrapper to manage a <see cref="IDbConnection"/>
    /// </summary>
    public class DbHelper : IDbHelper
    {
        private readonly static Lazy<IConnectionProvider> ConnectionProvider = new Lazy<IConnectionProvider>(
            () => Locator.Current.Instance<IConnectionProvider>());

        private readonly static Lazy<ICommanderFactory> StoredProcedureCreator = new Lazy<ICommanderFactory>(
            () => Locator.Current.Instance<ICommanderFactory>());
        
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
                    _connection = ConnectionProvider.Value.CreateConnection(ConnectionType.Web, null);
                    _connection.Open();
                }

                return _connection;
            }
        }

        public TResult ReturnValue<TResult, TInput>(Func<TInput, IStoredMethod> spGenerator) where TInput : class
        {
            var storedMethod = spGenerator.Invoke(Locator.Current.Instance<TInput>());

            var commander = StoredProcedureCreator.Value.GetStoredMethodCommander();

            return commander.Execute<TResult>(Connection, storedMethod);
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
