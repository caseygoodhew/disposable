namespace Disposable.Data.Database
{
    /// <summary>
    /// Interface for implementations which can generate concrete <see cref="IStoredMethodCommander"/>
    /// </summary>
    public interface ICommanderFactory
    {
        /// <summary>
        /// Gets a <see cref="IStoredMethodCommander"/>
        /// </summary>
        /// <returns>A <see cref="IStoredMethodCommander"/></returns>
        IStoredMethodCommander GetStoredMethodCommander();
    }
}
