using System;
using System.Data;

namespace Disposable.DataAccess.Database
{
    internal interface IDbConnection : IDisposable
    {
        ConnectionState State { get; }

        void Open();

        void Close();
    }
}
