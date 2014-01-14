using Disposable.Common.ServiceLocator;

namespace Disposable.DataAccess
{
    public static class Registrar
    {
        public static void Register(IRegistrar registrar)
        {
            Database.Oracle.Oracle.Register(registrar);
        }
    }
}
