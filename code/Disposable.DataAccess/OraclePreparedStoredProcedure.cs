using Disposable.Packages.Core;

namespace Disposable.DataAccess
{
    internal class OraclePreparedStoredProcedure : IPreparedStoredProcedure
    {
        private readonly IStoredProcedure _storedProcedure;
        
        internal OraclePreparedStoredProcedure(IStoredProcedure storedProcedure)
        {
            _storedProcedure = storedProcedure;
        }

        /*internal string CommandText()
        {
            return string.Format("{0}.{1}_{2}", Schemas.Disposable, _definition.Package, _definition.Procedure);
        }

        internal IEnumerable<SqlParameter> InputParameters()
        {
            return _parameters.Select(x => new SqlParameter
            {
                DbType = x.Key.DataType,
                Direction = ParameterDirection.Input,
                ParameterName = x.Key.Name,
                Value = x.Value
            });
        }

        internal SqlParameter OutputParameter()
        {
            if (_definition.OutputParameter != null)
            {
                return new SqlParameter
                {
                    DbType = _definition.OutputParameter.DataType,
                    Direction = ParameterDirection.Output,
                    ParameterName = _definition.OutputParameter.Name
                };
            }

            return null;
        }

        internal DbType GetReturnDataType()
        {
            if (_definition.OutputParameter == null)
            {
                throw new InvalidOperationException("No output parameter defined");
            }

            return _definition.OutputParameter.DataType;
        }*/
    }
}
