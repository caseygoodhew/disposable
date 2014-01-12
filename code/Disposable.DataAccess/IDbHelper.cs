using System;
using Disposable.Packages.Core;

namespace Disposable.DataAccess
{
    public interface IDbHelper : IDisposable
    {
        bool ReturnBool(IStoredProcedure storedProcedure);
    }
}
