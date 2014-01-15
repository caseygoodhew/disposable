using System;
using System.Data;

namespace Disposable.Data.Access.Database
{
    internal interface IDbConnection : IDisposable
    {
        ConnectionState State { get; }

        void Open();

        void Close();
    }
}
