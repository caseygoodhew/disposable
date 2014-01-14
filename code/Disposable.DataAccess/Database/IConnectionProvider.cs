using System;

namespace Disposable.DataAccess.Database
{
    internal interface IConnectionProvider
    {
        IDbConnection CreateConnection(ConnectionType connectionType, DbContext databaseContext);

        IDbConnection CreateConnection(ConnectionType connectionType, DbContext databaseContext, Guid authorizationToken);
    }
}
