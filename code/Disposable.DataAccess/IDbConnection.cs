using System;
using System.Data;

namespace Disposable.DataAccess
{
    public interface IDbConnection : IDisposable
    {
        ConnectionState State { get; }

        void Open();

        void Close();
    }
}
