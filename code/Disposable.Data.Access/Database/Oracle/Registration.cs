using Disposable.Common.ServiceLocator;

namespace Disposable.Data.Access.Database.Oracle
{
    internal static class Registration
    {
        internal static void Register(IRegistrar registrar)
        {
            registrar.Register<ICommanderFactory>(() => new OracleCommanderFactory());
            registrar.Register<IConnectionProvider>(() => new OracleConnectionProvider());
        }
    }
}
