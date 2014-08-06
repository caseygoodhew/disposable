using System;
using System.Collections.Generic;
using System.Linq;

using Disposable.Common;
using Disposable.Data.Common.Exceptions;

namespace Disposable.Data.Packages.Core
{
    public class StoredMethodInstance : IStoredMethodInstance
    {
        private readonly IStoredMethod storedMethod;

        private readonly IList<IInputParameterValue> inputParameterValues;

        private readonly IList<IOutputParameterValue> outputParameterValues;

        public StoredMethodInstance(IStoredMethod storedMethod) : this(storedMethod, null, null)
        {
        }

        public StoredMethodInstance(IStoredMethod storedMethod, IEnumerable<IInputParameter> inputParameters)
            : this(storedMethod, inputParameters, null)
        {
        }

        public StoredMethodInstance(IStoredMethod storedMethod, IEnumerable<IOutputParameter> outputParameters)
            : this(storedMethod, null, outputParameters)
        {
        }

        public StoredMethodInstance(
            IStoredMethod storedMethod,
            IEnumerable<IInputParameter> inputParameters,
            IEnumerable<IOutputParameter> outputParameters)
        {
            Guard.ArgumentNotNull(storedMethod, "storedMethod");
            
            this.storedMethod = storedMethod;

            inputParameterValues = inputParameters == null
                                       ? new List<IInputParameterValue>()
                                       : inputParameters.Select(x => new InputParameterValue(x) as IInputParameterValue).ToList();

            outputParameterValues = outputParameters == null
                                        ? new List<IOutputParameterValue>()
                                        : outputParameters.Select(x => new OutputParameterValue(x) as IOutputParameterValue).ToList();
        }

        /// <summary>
        /// Gets the <see cref="IPackage"/> which owns the <see cref="IStoredMethod"/>
        /// </summary>
        public IPackage Package
        {
            get
            {
                return storedMethod.Package; 
            }
        }

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        public string Name
        {
            get
            {
                return storedMethod.Name;
            }
        }

        /// <summary>
        /// Instructs an <see cref="IStoredMethod"/> to handle a <see cref="ProgrammaticDatabaseException"/> which 
        /// was thrown in the database when the <see cref="IStoredMethodInstance"/> was invoked.
        /// </summary>
        /// <param name="exceptionDescription">The <see cref="ExceptionDescription"/> of the error to handle.</param>
        /// <param name="underlyingDatabaseException">The <see cref="UnderlyingDatabaseException"/></param>
        /// <returns>
        /// If the exception has been handled, this should be the same value that was passed into the method. 
        /// Returning any value other than this will result in an <see cref="UnhandledDatabaseException"/> being immediately thrown.
        /// </returns>
        public ExceptionDescription Handle(
            ExceptionDescription exceptionDescription,
            UnderlyingDatabaseException underlyingDatabaseException)
        {
            return storedMethod.Handle(this, exceptionDescription, underlyingDatabaseException);
        }

        /// <summary>
        /// Set a named parameter value of the given type.
        /// </summary>
        /// <typeparam name="TParameterValue">The type of <see cref="IParameterValue"/> to set.</typeparam>
        /// <param name="name">The name of the parameter to set.</param>
        /// <param name="value">The parameter value.</param>
        public void SetValue<TParameterValue>(string name, object value) where TParameterValue : IParameterValue
        {
            SetValue(GetStore<TParameterValue>(), name, value);
        }

        /// <summary>
        /// Set named parameter values of the given type.
        /// </summary>
        /// <typeparam name="TParameterValue">The type of <see cref="IParameterValue"/> to set.</typeparam>
        /// <param name="values">An dictionary of values to set.</param>
        public void SetValues<TParameterValue>(IDictionary<string, object> values) where TParameterValue : IParameterValue
        {
            var store = GetStore<TParameterValue>();
            values.ToList().ForEach(x => SetValue(store, x.Key, x.Value));
        }

        /// <summary>
        /// Gets parameter values of the given type.
        /// </summary>
        /// <typeparam name="TParameterValue">The type of <see cref="IParameterValue"/> to get.</typeparam>
        /// <param name="includeEmpty">Flag to include parameters with no value set.</param>
        /// <returns>A List of <see cref="TParameterValue"/>.</returns>
        public IList<TParameterValue> GetValues<TParameterValue>(bool includeEmpty) where TParameterValue : IParameterValue
        {
            var store = GetStore<TParameterValue>();

            if (includeEmpty)
            {
                return store;
            }

            return store.Where(x => x.Value != null).ToList();
        }

        /// <summary>
        /// Gets a named parameter value of the given type.
        /// </summary>
        /// <typeparam name="TParameterValue">The type of <see cref="IParameterValue"/> to get.</typeparam>
        /// <param name="name">The name of the parameter to get.</param>
        /// <returns>The <see cref="TParameterValue"/></returns>
        public TParameterValue GetValue<TParameterValue>(string name) where TParameterValue : IParameterValue
        {
            var store = GetStore<TParameterValue>();
            return store.Single(x => x.Name == name);
        }

        private void SetValue<TParameterValue>(IList<TParameterValue> store, string name, object value)
            where TParameterValue : IParameterValue
        {
            var parameterValue = store.Single(x => x.Name == name);

            if (parameterValue.Value != null)
            {
                throw new InvalidOperationException(string.Format("Parameter {0} value is already set to {1}.", name, parameterValue.Value));
            }

            parameterValue.Value = value;
        }
        
        private IList<TParameterValue> GetStore<TParameterValue>() where TParameterValue : IParameterValue
        {
            var parameterValueType = typeof(TParameterValue);

            if (typeof(IInputParameterValue).IsAssignableFrom(parameterValueType))
            {
                return (IList<TParameterValue>)inputParameterValues.Cast<TParameterValue>();
            }

            if (typeof(IOutputParameterValue).IsAssignableFrom(parameterValueType))
            {
                return (IList<TParameterValue>)outputParameterValues.Cast<TParameterValue>();
            }

            throw new InvalidOperationException(string.Format("Unknown IParameterValue type {0}", parameterValueType.Name));
        }
    }
}
