using System.Data;
using Oracle.DataAccess.Client;

namespace Disposable.Data.Access.Database.Oracle
{
    /// <summary>
    /// Acts as an adapter to the underlying oracle database connection
    /// </summary>
    internal class OracleDbConnection : IDbConnection
    {
        /// <summary>
        /// The underlying <see cref="OracleConnection"/>.
        /// </summary>
        internal readonly OracleConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="OracleDbConnection"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to use to connect to the underlying database connection</param>
        public OracleDbConnection(string connectionString)
        {
            Connection = new OracleConnection(connectionString);
        }

        /// <summary>
        /// Gets the underlying database connection state
        /// </summary>
        public ConnectionState State
        {
            get { return Connection.State; }
        }
        
        /// <summary>
        /// Opens the underlying database connection
        /// </summary>
        public void Open()
        {
            Connection.Open();
        }

        /// <summary>
        /// Closes the underlying database connection
        /// </summary>
        public void Close()
        {
            Connection.Close();
        }

        /// <summary>
        /// Disposes of the underlying database connection
        /// </summary>
        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }

            Connection.Dispose();
        }
    }
}
