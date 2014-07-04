using System;

namespace Disposable.Data.Access.Database
{
    /// <summary>
    /// Interface for implementations which can create database connections.
    /// </summary>
    internal interface IConnectionProvider
    {
        /// <summary>
        /// Creates a database connection.
        /// </summary>
        /// <param name="connectionSource">The source of the connection.</param>
        /// <param name="databaseContext">The database context</param>
        /// <returns>A database connection</returns>
        IDbConnection CreateConnection(ConnectionSource connectionSource, DbContext databaseContext);

        /// <summary>
        /// Creates a database connection.
        /// </summary>
        /// <param name="connectionSource">The source of the connection.</param>
        /// <param name="databaseContext">The database context</param>
        /// <param name="authorizationToken">The user authorization token.</param>
        /// <returns>A database connection</returns>
        IDbConnection CreateConnection(ConnectionSource connectionSource, DbContext databaseContext, Guid authorizationToken);
    }
}
