using Disposable.Common.ServiceLocator;
using Disposable.Data.Packages.User;

namespace Disposable.Data.Packages
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
            registrar.Register<IUserPackage>(() => new UserPackage());
        }
    }
}
