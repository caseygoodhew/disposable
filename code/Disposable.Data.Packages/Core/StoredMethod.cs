using System;
using System.Collections.Generic;
using System.Linq;
using Disposable.Common.Extensions;
using Disposable.Data.Common.Exceptions;

namespace Disposable.Data.Packages.Core
{
    internal abstract class StoredMethod : IStoredMethod
    {
        private IList<InputParameterValue> _lastFullParameterValues;
        
        private IList<InputParameterValue> _fullParameterValues;

        private IList<InputParameterValue> _trimmedParameterValues;

        private readonly IList<IInputParameter> _inputParameters;

        public IPackage Package { get; private set; }

        public string Name { get; private set; }
        
        protected StoredMethod(IPackage package, string name, params IInputParameter[] inputParameters)
        {
            Package = package;
            Name = name;
            _inputParameters = inputParameters.ToList();
        }

        protected void SetParameterValues(IDictionary<string, object> parameterValues)
        {
            var inputParameters = (_inputParameters ?? Enumerable.Empty<IInputParameter>()).ToList();

            if (inputParameters.IsNullOrEmpty() && parameterValues.IsNullOrEmpty())
            {
                _trimmedParameterValues = new List<InputParameterValue>();
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

                _fullParameterValues = parameterDictionary.Select(x => new InputParameterValue(parameterDictionary[x.Key], valueDictionary.ContainsKey(x.Key) ? valueDictionary[x.Key] : null)).ToList();
                _trimmedParameterValues = valueDictionary.Select(x => new InputParameterValue(parameterDictionary[x.Key], x.Value)).ToList();
            }
        }

        public IList<InputParameterValue> GetInputParameters(bool includeEmpty)
        {
            var parameters = includeEmpty ? _fullParameterValues : _trimmedParameterValues;

            _lastFullParameterValues = _fullParameterValues;
            _trimmedParameterValues = null;
            _fullParameterValues = null;

            return parameters;
        }

        public InputParameterValue GetInputParameterValue(string name)
        {
            return (_fullParameterValues ?? _lastFullParameterValues).Single(x => x.Name == name);
        }

        public virtual ProgrammaticDatabaseExceptions Handle(ProgrammaticDatabaseExceptions programmaticDatabaseException)
        {
            return ProgrammaticDatabaseExceptions.Unhandled;
        }
    }
}
