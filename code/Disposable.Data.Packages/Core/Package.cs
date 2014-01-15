using Disposable.Common.ServiceLocator;

namespace Disposable.Data.Packages.Core
{
    public abstract class Package : BaseRegistrar, IPackage
    {
        public abstract string Schema { get; }

        public abstract string Name { get; }
    }
}
