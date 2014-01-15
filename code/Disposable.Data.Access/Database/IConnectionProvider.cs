using System;

namespace Disposable.Data.Access.Database
{
    internal interface IConnectionProvider
    {
        IDbConnection CreateConnection(ConnectionType connectionType, DbContext databaseContext);

        IDbConnection CreateConnection(ConnectionType connectionType, DbContext databaseContext, Guid authorizationToken);
    }
}
