using System;
using System.Collections;

namespace Disposable.DataAccess.Database.Oracle
{
    internal class OracleConnectionProvider : IConnectionProvider
    {
        private static readonly string WebConnectionString = "Password=upd;Persist Security Info=True;User ID=upd;Data Source=disposable;";
        private static readonly string SecurityConnectionString = "Password=upd;Persist Security Info=True;User ID=upd;Data Source=disposable;";

        public IDbConnection CreateConnection(ConnectionType connectionType, DbContext databaseContext)
        {
            switch (connectionType)
            {
                case ConnectionType.Web:
                    return new OracleDbConnection(WebConnectionString);
                default:
                    throw new ArgumentOutOfRangeException("connectionType");
            }
        }

        public IDbConnection CreateConnection(ConnectionType connectionType, DbContext databaseContext, Guid authorizationToken)
        {
            throw new NotImplementedException();
        }
    }
}
