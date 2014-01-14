using Disposable.Common.ServiceLocator;
using Disposable.DataAccess.Packages.User;

namespace Disposable.DataAccess.Packages
{
    public static class Registrar
    {
        public static void Register(IRegistrar registrar)
        {
            registrar.Register<IUserPackage>(() => new UserPackage());
        }
    }
}
