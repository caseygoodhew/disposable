using Disposable.DataAccess.Packages.Core;

namespace Disposable.DataAccess.Database
{
    internal interface IStoredProcedureAdapter<out T> where T : IPreparedStoredProcedure
    {
        T Prepare(IStoredProcedure storedProcedure);
    }
}
