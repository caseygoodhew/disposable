using System;
using System.Collections.Generic;
using Disposable.DataAccess.StoredProcedures;

namespace Disposable.DataAccess
{
    public interface IDbHelper : IDisposable
    {
        bool ReturnBool<T>(IDictionary<string, object> parameters) where T : IStoredProcedureDefinition, new();
    }
}
