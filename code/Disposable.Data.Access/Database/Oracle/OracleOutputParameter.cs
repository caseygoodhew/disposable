using Disposable.Data.Packages.Core;
using Oracle.DataAccess.Client;

namespace Disposable.Data.Access.Database.Oracle
{
    internal class OracleOutputParameter : OutputParameterValue
    {
        public readonly OracleParameter OracleParameter;
        
        public OracleOutputParameter(OracleParameter oracleParameter, IOutputParameter parameter) : base(parameter)
        {
            OracleParameter = oracleParameter;
        }
    }
}
