using Disposable.Common.ServiceLocator;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Abstract implementation of a database package.
    /// </summary>
    public abstract class Package : BaseRegistrar, IPackage
    {
        /// <summary>
        /// Gets the package schema name.
        /// </summary>
        public abstract string Schema { get; }

        /// <summary>
        /// Gets the package name.
        /// </summary>
        public abstract string Name { get; }
    }
}
