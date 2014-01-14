using Disposable.DataAccess.Packages.Core;

namespace Disposable.DataAccess.Database.Oracle
{
    internal class OraclePreparedStoredProcedure : IPreparedStoredProcedure
    {
        private readonly IStoredProcedure _storedProcedure;
        
        internal OraclePreparedStoredProcedure(IStoredProcedure storedProcedure)
        {
            _storedProcedure = storedProcedure;
        }

        private string CommandText()
        {
            return string.Format("{0}.{1}.{2}", _storedProcedure.Package.Schema, _storedProcedure.Package.Name, _storedProcedure.Name);
        }
        /*
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
