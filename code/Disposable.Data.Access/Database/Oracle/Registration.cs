using Disposable.Common.ServiceLocator;

namespace Disposable.Data.Access.Database.Oracle
{
    /// <summary>
    /// Responsible for registering all services provided by this project.
    /// </summary>
    internal static class Registration
    {
        /// <summary>
        /// Registers all services provided by this project.
        /// </summary>
        /// <param name="registrar">The registrar.</param>
        internal static void Register(IRegistrar registrar)
        {
            registrar.Register<ICommanderFactory>(() => new OracleCommanderFactory());
            registrar.Register<IConnectionProvider>(() => new OracleConnectionProvider());
            registrar.Register<IDataObjectConverter>(() => new OracleDataObjectConverter());
        }
    }
}
