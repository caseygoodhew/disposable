using System;
using Disposable.DataAccess.Packages.Core;

namespace Disposable.DataAccess
{
    public interface IDbHelper : IDisposable
    {
        bool ReturnBool(IStoredProcedure storedProcedure);
    }
}
