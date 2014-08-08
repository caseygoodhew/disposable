using Disposable.Common.ServiceLocator;
using Disposable.Common.Services;

namespace Disposable.Common
{
    public static class Registration
    {
        public static void Register(IRegistrar registrar)
        {
            registrar.Register<ITimeSource>(() => new LocalTimeSource());
        }
    }
}
