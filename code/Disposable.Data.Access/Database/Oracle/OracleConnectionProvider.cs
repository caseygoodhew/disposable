using System;
using System.Collections;

namespace Disposable.Data.Access.Database.Oracle
{
    internal class OracleConnectionProvider : IConnectionProvider
    {
        private static readonly string WebConnectionString = "Password=upd;Persist Security Info=True;User ID=upd;Data Source=disposable;";
        private static readonly string SecurityConnectionString = "Password=upd;Persist Security Info=True;User ID=upd;Data Source=disposable;";

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

        public IDbConnection CreateConnection(ConnectionSource connectionSource, DbContext databaseContext, Guid authorizationToken)
        {
            throw new NotImplementedException();
        }
    }
}
