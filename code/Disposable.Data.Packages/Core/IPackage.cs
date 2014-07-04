namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Interface for a database package.
    /// </summary>
    public interface IPackage
    {
        /// <summary>
        /// Gets the package schema name.
        /// </summary>
        string Schema { get; }

        /// <summary>
        /// Gets the package name.
        /// </summary>
        string Name { get; }
    }
}
