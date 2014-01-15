using Disposable.Common.ServiceLocator;

namespace Disposable.Data.Access.Database.Oracle
{
    internal static class Registration
    {
        internal static void Register(IRegistrar registrar)
        {
            registrar.Register<ICommanderCreator>(() => new OracleCommanderCreator());
            registrar.Register<IConnectionProvider>(() => new OracleConnectionProvider());
        }
    }
}
