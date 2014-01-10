﻿using System.Data;
using Oracle.DataAccess.Client;

namespace Disposable.DataAccess
{
    /// <summary>
    /// Acts as an adapter to the underlying database connection
    /// </summary>
    internal class DbConnection : IDbConnection
    {
        /// <summary>
        /// The underlying database connection
        /// </summary>
        private readonly OracleConnection _oracleConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnection"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to use to connect to the underlying database connection</param>
        public DbConnection(string connectionString)
        {
            _oracleConnection = new OracleConnection(connectionString);
        }

        /// <summary>
        /// Gets the underlying database connection state
        /// </summary>
        public ConnectionState State
        {
            get { return _oracleConnection.State; }
        }
        
        /// <summary>
        /// Opens the underlying database connection
        /// </summary>
        public void Open()
        {
            _oracleConnection.Open();
        }

        /// <summary>
        /// Closes the underlying database connection
        /// </summary>
        public void Close()
        {
            _oracleConnection.Close();
        }

        /// <summary>
        /// Disposes of the underlying database connection
        /// </summary>
        public void Dispose()
        {
            if (_oracleConnection.State != ConnectionState.Closed)
            {
                _oracleConnection.Close();
            }

            _oracleConnection.Dispose();
        }
    }
}