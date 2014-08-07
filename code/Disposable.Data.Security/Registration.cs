using Disposable.Common.ServiceLocator;
using Disposable.Data.Security.Accounts;
using Disposable.Data.Security.Packages.User;

namespace Disposable.Data.Security
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
            registrar.Register<IAccountRepository>(() => new AccountRepository());

            registrar.Register<IUserPackage>(() => new UserPackage());
        }
    }
}
