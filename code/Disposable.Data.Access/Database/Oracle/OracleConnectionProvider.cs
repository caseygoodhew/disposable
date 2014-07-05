using System;

namespace Disposable.Data.Access.Database.Oracle
{
    /// <summary>
    /// Factory for creating oracle database connections.
    /// </summary>
    internal class OracleConnectionProvider : IConnectionProvider
    {
        private static readonly string WebConnectionString = "Password=upd;Persist Security Info=True;User ID=upd;Data Source=disposable;";
        //// private static readonly string SecurityConnectionString = "Password=upd;Persist Security Info=True;User ID=upd;Data Source=disposable;";

        /// <summary>
        /// Creates an oracle database connection.
        /// </summary>
        /// <param name="connectionSource">The source of the connection.</param>
        /// <param name="databaseContext">The database context.</param>
        /// <returns>A database connection.</returns>
        public IDbConnection CreateConnection(ConnectionSource connectionSource, DbContext databaseContext)
        {
            switch (connectionSource)
            {
                case ConnectionSource.Web:
                    return new OracleDbConnection(WebConnectionString);
                default:
                    throw new ArgumentOutOfRangeException("connectionSource");
            }
        }

        /// <summary>
        /// Creates an oracle database connection.
        /// </summary>
        /// <param name="connectionSource">The source of the connection.</param>
        /// <param name="databaseContext">The database context.</param>
        /// <param name="authorizationToken">The user authorization token.</param>
        /// <returns>A database connection.</returns>
        public IDbConnection CreateConnection(ConnectionSource connectionSource, DbContext databaseContext, Guid authorizationToken)
        {
            throw new NotImplementedException();
        }
    }
}
