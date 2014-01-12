using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disposable.Packages.Core;

namespace Disposable.DataAccess
{
    internal class OracleStoredProcedureAdapter : IStoredProcedureAdapter<OraclePreparedStoredProcedure>
    {
        public OraclePreparedStoredProcedure Prepare(IStoredProcedure storedProcedure)
        {
            return new OraclePreparedStoredProcedure(storedProcedure);
        }
    }
}
