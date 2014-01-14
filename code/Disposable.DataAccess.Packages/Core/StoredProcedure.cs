using System;
using System.Collections.Generic;
using System.Linq;
using Disposable.Common.Extensions;

namespace Disposable.DataAccess.Packages.Core
{
    internal abstract class StoredProcedure : IStoredProcedure
    {
        private IDictionary<InputParameter, object> _parameters = null;

        public IPackage Package { get; private set; }

        public string Name { get; private set; }
        
        public IList<InputParameter> InputParameters { get; private set; }

        public OutputParameter OutputParameter { get; private set; }

        protected StoredProcedure(IPackage package, string name, params InputParameter[] inputParameters)
            : this(package, name, null, inputParameters)
        {
            
        }

        protected StoredProcedure(IPackage package, string name, OutputParameter outputParameter, params InputParameter[] inputParameters)
        {
            Package = package;
            Name = name;
            OutputParameter = outputParameter;
            InputParameters = inputParameters.ToList();
        }

        protected void SetParameterValues(IDictionary<string, object> parameterValues)
        {
            var inputParameters = (InputParameters ?? Enumerable.Empty<InputParameter>()).ToList();

            if (inputParameters.IsNullOrEmpty() && parameterValues.IsNullOrEmpty())
            {
                _parameters = new Dictionary<InputParameter, object>();
            }
            else
            {
                var valueDictionary = (parameterValues ?? new Dictionary<string, object>()).ToDictionary(x => x.Key, x => x.Value, StringComparer.InvariantCultureIgnoreCase);

                var parameterDictionary = inputParameters.ToDictionary(x => x.Name, StringComparer.InvariantCultureIgnoreCase);

                var requiredParameters = parameterDictionary.Where(x => x.Value.Required).Select(x => x.Key).ToList();
                var missingParameters = requiredParameters.Where(x => !valueDictionary.ContainsKey(x)).ToList();

                if (missingParameters.Any())
                {
                    var message = string.Format("The following parameterValues are required but were not supplied: {0}", string.Join(", ", missingParameters));
                    throw new ArgumentException(message, "parameterValues");
                }

                var allParameters = parameterDictionary.Select(x => x.Key);
                var extraParameters = valueDictionary.Where(x => !allParameters.Contains(x.Key)).ToList();

                if (extraParameters.Any())
                {
                    var message = string.Format("The following supplied parameterValues are not defined: {0}", string.Join(", ", extraParameters));
                    throw new ArgumentException(message, "parameterValues");
                }

                _parameters = valueDictionary.ToDictionary(x => parameterDictionary[x.Key], x => x.Value);
            }
        }

        public IDictionary<InputParameter, object> GetParameters()
        {
            var parameters = _parameters;

            _parameters = null;

            return parameters;
        }
    }
}
