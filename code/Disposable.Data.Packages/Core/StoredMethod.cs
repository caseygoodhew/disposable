using System;
using System.Collections.Generic;
using System.Linq;
using Disposable.Common.Extensions;
using Disposable.Data.Common.Exceptions;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Abstract class defining the common attributes required to call a stored method.
    /// </summary>
    internal abstract class StoredMethod : IStoredMethod
    {
        private readonly IList<IInputParameter> inputParameters;

        private IList<InputParameterValue> lastFullParameterValues;
        
        private IList<InputParameterValue> fullParameterValues;

        private IList<InputParameterValue> trimmedParameterValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredMethod"/> class.
        /// </summary>
        /// <param name="package">The <see cref="IPackage"/> that contains the procedure.</param>
        /// <param name="name">The name of the procedure.</param>
        /// <param name="inputParameters">The list of <see cref="IInputParameter"/>s in the package declaration.</param>
        protected StoredMethod(IPackage package, string name, params IInputParameter[] inputParameters)
        {
            Package = package;
            Name = name;
            this.inputParameters = inputParameters.ToList();
        }

        /// <summary>
        /// Gets the <see cref="IPackage"/> which owns the <see cref="IStoredProcedure"/>
        /// </summary>
        public IPackage Package { get; private set; }

        /// <summary>
        /// Gets the name of the procedure
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the input parameter values.
        /// </summary>
        /// <param name="includeEmpty">Flag to include empty parameters.</param>
        /// <returns>A list of <see cref="InputParameterValue"/></returns>
        public IList<InputParameterValue> GetInputParameterValues(bool includeEmpty)
        {
            var parameters = includeEmpty ? this.fullParameterValues : this.trimmedParameterValues;

            this.lastFullParameterValues = this.fullParameterValues;
            this.trimmedParameterValues = null;
            this.fullParameterValues = null;

            return parameters;
        }

        /// <summary>
        /// Gets an input parameter value.
        /// </summary>
        /// <param name="name">The name of the parameter to get.</param>
        /// <returns>The <see cref="InputParameterValue"/></returns>
        public InputParameterValue GetInputParameterValue(string name)
        {
            return (this.fullParameterValues ?? this.lastFullParameterValues).Single(x => x.Name == name);
        }

        /// <summary>
        /// Instructs an <see cref="IStoredMethod"/> to handle a <see cref="ProgrammaticDatabaseException"/> which 
        /// was thrown in the database when the <see cref="IStoredMethod"/> was invoked.
        /// This method should be overridden in derived classes that provide error handling.
        /// </summary>
        /// <param name="programmaticDatabaseException">The normalized <see cref="ProgrammaticDatabaseException"/>.</param>
        /// <returns>
        /// <see cref="ProgrammaticDatabaseExceptions.Unhandled"/> if not overridden.
        /// </returns>
        public virtual ProgrammaticDatabaseExceptions Handle(ProgrammaticDatabaseExceptions programmaticDatabaseException)
        {
            return ProgrammaticDatabaseExceptions.Unhandled;
        }

        /// <summary>
        /// Sets the parameter values by key value dictionary.
        /// </summary>
        /// <param name="parameterValues">Key value dictionary of parameter names and values.</param>
        protected void SetInputParameterValues(IDictionary<string, object> parameterValues)
        {
            var inputParameters = (this.inputParameters ?? Enumerable.Empty<IInputParameter>()).ToList();

            if (inputParameters.IsNullOrEmpty() && parameterValues.IsNullOrEmpty())
            {
                this.trimmedParameterValues = new List<InputParameterValue>();
            }
            else
            {
                var valueDictionary = (parameterValues ?? new Dictionary<string, object>()).ToDictionary(
                    x => x.Key,
                    x => x.Value,
                    StringComparer.InvariantCultureIgnoreCase);

                var parameterDictionary = inputParameters.ToDictionary(
                    x => x.Name,
                    StringComparer.InvariantCultureIgnoreCase);

                var requiredParameters = parameterDictionary.Where(x => x.Value.Required).Select(x => x.Key).ToList();
                var missingParameters = requiredParameters.Where(x => !valueDictionary.ContainsKey(x)).ToList();

                if (missingParameters.Any())
                {
                    var message = string.Format(
                        "The following parameterValues are required but were not supplied: {0}",
                        string.Join(", ", missingParameters));
                    throw new ArgumentException(message, "parameterValues");
                }

                var allParameters = parameterDictionary.Select(x => x.Key);
                var extraParameters = valueDictionary.Where(x => !allParameters.Contains(x.Key)).ToList();

                if (extraParameters.Any())
                {
                    var message = string.Format(
                        "The following supplied parameterValues are not defined: {0}",
                        string.Join(", ", extraParameters));
                    throw new ArgumentException(message, "parameterValues");
                }

                this.fullParameterValues =
                    parameterDictionary.Select(
                        x =>
                        new InputParameterValue(
                            parameterDictionary[x.Key],
                            valueDictionary.ContainsKey(x.Key) ? valueDictionary[x.Key] : null)).ToList();
                this.trimmedParameterValues =
                    valueDictionary.Select(x => new InputParameterValue(parameterDictionary[x.Key], x.Value)).ToList();
            }
        }
    }
}
