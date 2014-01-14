using Disposable.DataAccess.Packages.Core;

namespace Disposable.DataAccess.Database.Oracle
{
    internal class OracleStoredProcedureAdapter : IStoredProcedureAdapter<OraclePreparedStoredProcedure>
    {
        public OraclePreparedStoredProcedure Prepare(IStoredProcedure storedProcedure)
        {
            return new OraclePreparedStoredProcedure(storedProcedure);
        }
    }
}
