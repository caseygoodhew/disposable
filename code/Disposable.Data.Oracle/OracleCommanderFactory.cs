using Disposable.Data.Database;

namespace Disposable.Data.Oracle
{
    /// <summary>
    /// Generates <see cref="OracleStoredMethodCommander"/>
    /// </summary>
    internal class OracleCommanderFactory : ICommanderFactory
    {
        /// <summary>
        /// Gets a <see cref="OracleStoredMethodCommander"/>
        /// </summary>
        /// <returns>A <see cref="OracleStoredMethodCommander"/></returns>
        public IStoredMethodCommander GetStoredMethodCommander()
        {
            return new OracleStoredMethodCommander();
        }
    }
}
