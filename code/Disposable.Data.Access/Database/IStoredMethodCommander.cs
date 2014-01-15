using Disposable.Data.Packages.Core;

namespace Disposable.Data.Access.Database
{
    internal interface IStoredMethodCommander
    {
        T Execute<T>(IDbConnection connection, IStoredMethod storedMethod);
    }
}
