using Disposable.Data.Packages.Core;

namespace Disposable.Data.Access.Database.Oracle
{
    internal class OracleCommanderCreator : ICommanderCreator
    {
        public IStoredMethodCommander CreateStoredMethodCommander()
        {
            return new OracleStoredMethodCommander();
        }
    }
}
