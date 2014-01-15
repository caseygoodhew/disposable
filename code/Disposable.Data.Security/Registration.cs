using Disposable.Common.ServiceLocator;
using Disposable.Data.Security.Accounts;

namespace Disposable.Data.Security
{
    public static class Registration
    {
        public static void Register(IRegistrar registrar)
        {
            registrar.Register<IAccountRepository>(() => new AccountRepository());
        }
    }
}
