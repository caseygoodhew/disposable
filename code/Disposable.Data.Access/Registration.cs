using Disposable.Common.ServiceLocator;

namespace Disposable.Data.Access
{
    /// <summary>
    /// Responsible for registering all services provided by this project.
    /// </summary>
    public static class Registration
    {
        /// <summary>
        /// Registers all services provided by this project.
        /// </summary>
        /// <param name="registrar">The registrar.</param>
        public static void Register(IRegistrar registrar)
        {
            Database.Oracle.Registration.Register(registrar);
        }
    }
}
