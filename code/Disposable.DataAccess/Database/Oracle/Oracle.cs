using Disposable.Common.ServiceLocator;

namespace Disposable.DataAccess.Database.Oracle
{
    internal static class Oracle
    {
        internal static void Register(IRegistrar registrar)
        {
            registrar.Register<IStoredProcedureAdapter<IPreparedStoredProcedure>>(() => new OracleStoredProcedureAdapter());
            registrar.Register<IConnectionProvider>(() => new OracleConnectionProvider());
        }
    }
}
