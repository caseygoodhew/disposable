using System;
using System.Data;

namespace Disposable.Data.Database
{
    /// <summary>
    /// Interface for implementations of database connections
    /// </summary>
    public interface IDbConnection : IDisposable
    {
        /// <summary>
        /// Gets the connection state.
        /// </summary>
        ConnectionState State { get; }

        /// <summary>
        /// Opens a connection.
        /// </summary>
        void Open();

        /// <summary>
        /// Closes a connection.
        /// </summary>
        void Close();
    }
}
