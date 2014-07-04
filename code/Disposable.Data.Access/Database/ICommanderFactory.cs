using Disposable.Data.Packages.Core;

namespace Disposable.Data.Access.Database
{
    /// <summary>
    /// Interface for implementations which can generate concrete <see cref="IStoredMethodCommander"/>
    /// </summary>
    internal interface ICommanderFactory
    {
        /// <summary>
        /// Gets a <see cref="IStoredMethodCommander"/>
        /// </summary>
        /// <returns>A <see cref="IStoredMethodCommander"/></returns>
        IStoredMethodCommander GetStoredMethodCommander();
    }
}
