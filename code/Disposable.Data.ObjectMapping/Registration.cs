using Disposable.Common.ServiceLocator;

namespace Disposable.Data.ObjectMapping
{
    public static class Registration
    {
        public static void Register(IRegistrar registrar)
        {
            registrar.Register<IObjectMapper>(() => new ObjectMapper());
        }
    }
}
