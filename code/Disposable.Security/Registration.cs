using Disposable.Common.ServiceLocator;
using Disposable.Security.Accounts;
using Disposable.Security.Authentication;
using Disposable.Security.Policies;

namespace Disposable.Security
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
            registrar.Register<IAuthentication>(() => new Authentication.Authentication());
            registrar.Register<IUserAccountManager>(() => new UserAccountManager());

            registrar.Register<IPasswordPolicy>(() => new PasswordPolicy());
        }
    }
}
