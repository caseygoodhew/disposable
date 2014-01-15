using Disposable.Common.ServiceLocator;
using Disposable.Data.Packages.User;

namespace Disposable.Data.Packages
{
    public static class Registration
    {
        public static void Register(IRegistrar registrar)
        {
            registrar.Register<IUserPackage>(() => new UserPackage());
        }
    }
}
