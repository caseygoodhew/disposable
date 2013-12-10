using Disposable.Common;
using Disposable.Common.ServiceLocator;

namespace Disposable.Test.Common.ServiceLocator
{
    public static class LocatorExtensions
    {
        public static void ResetRegsitrars(this Locator locator)
        {
            Guard.ArgumentNotNull(locator, "locator");
            
            locator.BaseRegistrar = new BaseRegistrar();
            locator.OverrideRegistrar = new OverrideRegistrar();
        }

        public static void ResetRegsitrars(this ILocator iLocator)
        {
            Guard.ArgumentNotNull(iLocator, "iLocator");
            
            (iLocator as Locator).ResetRegsitrars();
        }
    }
}
