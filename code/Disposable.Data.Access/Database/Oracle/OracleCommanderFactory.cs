namespace Disposable.Data.Access.Database.Oracle
{
    internal class OracleCommanderFactory : ICommanderFactory
    {
        public IStoredMethodCommander GetStoredMethodCommander()
        {
            return new OracleStoredMethodCommander();
        }
    }
}
