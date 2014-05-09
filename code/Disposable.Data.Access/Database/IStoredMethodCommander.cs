using System.Data;
using Disposable.Data.Packages.Core;

namespace Disposable.Data.Access.Database
{
    internal interface IStoredMethodCommander
    {
        T Execute<T>(IDbConnection connection, IStoredMethod storedMethod);

        void Execute<TOut1, TOut2>(IDbConnection connection, IStoredMethod storedMethod, out TOut1 out1, out TOut2 out2);
    }
}
