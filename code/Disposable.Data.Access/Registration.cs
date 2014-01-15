using Disposable.Common.ServiceLocator;

namespace Disposable.Data.Access
{
    public static class Registration
    {
        public static void Register(IRegistrar registrar)
        {
            Database.Oracle.Registration.Register(registrar);
        }
    }
}
