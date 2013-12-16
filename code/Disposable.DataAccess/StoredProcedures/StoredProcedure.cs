using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disposable.Common;
using Disposable.Common.Linq;
using MySql.Data.MySqlClient;

namespace Disposable.DataAccess.StoredProcedures
{
    internal class StoredProcedure
    {
        private readonly IStoredProcedureDefinition _definition;

        private readonly IDictionary<InputParameter, object> _parameters;

        private StoredProcedure(IStoredProcedureDefinition definition, IDictionary<string, object> inputParameters)
        {
            Guard.ArgumentNotNull(definition, "definition");

            _definition = definition;

            var defintionInputParameters = (definition.InputParameters ?? Enumerable.Empty<InputParameter>()).ToList();

            if (defintionInputParameters.IsNullOrEmpty() && inputParameters.IsNullOrEmpty())
            {
                _parameters = new Dictionary<InputParameter, object>();
            }
            else
            {
                var inputDictionary = (inputParameters ?? new Dictionary<string, object>()).ToDictionary(x => x.Key, x => x.Value, StringComparer.InvariantCultureIgnoreCase);

                var defintionInputDictionary = defintionInputParameters.ToDictionary(x => x.Name, StringComparer.InvariantCultureIgnoreCase);

                var requiredParameters = defintionInputDictionary.Where(x => x.Value.Required).Select(x => x.Key).ToList();
                var missingParameters = requiredParameters.Where(x => !inputDictionary.ContainsKey(x)).ToList();

                if (missingParameters.Any())
                {
                    var message = string.Format("The following inputParameters are required but were not supplied: {0}", string.Join(", ", missingParameters));
                    throw new ArgumentException(message, "inputParameters");
                }

                var allParameters = defintionInputDictionary.Select(x => x.Key);
                var extraParameters = inputDictionary.Where(x => !allParameters.Contains(x.Key)).ToList();

                if (extraParameters.Any())
                {
                    var message = string.Format("The following supplied inputParameters are not defined: {0}", string.Join(", ", extraParameters));
                    throw new ArgumentException(message, "inputParameters");
                }

                _parameters = inputDictionary.ToDictionary(x => defintionInputDictionary[x.Key], x => x.Value);
            }
        }

        internal static StoredProcedure Create<T>(IDictionary<string, object> parameters)
            where T : IStoredProcedureDefinition, new()
        {
            return new StoredProcedure(new T(), parameters);
        }

        internal string CommandText()
        {
            return string.Format("{0}.{1}_{2}", Schemas.Disposable, _definition.Package, _definition.Procedure);
        }

        internal IEnumerable<MySqlParameter> InputParameters()
        {
            return _parameters.Select(x => new MySqlParameter
            {
                MySqlDbType = x.Key.DataType,
                Direction = ParameterDirection.Input,
                ParameterName = x.Key.Name,
                Value = x.Value
            });
        }

        internal MySqlParameter OutputParameter()
        {
            if (_definition.OutputParameter != null)
            {
                return new MySqlParameter
                {
                    MySqlDbType = _definition.OutputParameter.DataType,
                    Direction = ParameterDirection.Output,
                    ParameterName = _definition.OutputParameter.Name
                };
            }

            return null;
        }

        internal MySqlDbType GetReturnDataType()
        {
            if (_definition.OutputParameter == null)
            {
                throw new InvalidOperationException("No output parameter defined");
            }

            return _definition.OutputParameter.DataType;
        }
    }
}
