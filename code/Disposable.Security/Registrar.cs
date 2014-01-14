using Disposable.Common.ServiceLocator;
using Disposable.Security.Accounts;
using Disposable.Security.Authentication;
using Disposable.Security.Policies;

namespace Disposable.Security
{
    public static class Registrar
    {
        public static void Register(IRegistrar registrar)
        {
            registrar.Register<IAuthentication>(() => new Authentication.Authentication());
            registrar.Register<IAccountManager>(() => new AccountManager());

            registrar.Register<IPasswordPolicy>(() => new PasswordPolicy());
        }
    }
}
