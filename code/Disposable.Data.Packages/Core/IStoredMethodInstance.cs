using System.Collections.Generic;

using Disposable.Data.Common.Exceptions;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Provides a single transaction value set and error handling for an <see cref="IStoredMethod"/>.
    /// </summary>
    public interface IStoredMethodInstance
    {
        /// <summary>
        /// Gets the <see cref="IPackage"/> which owns the <see cref="IStoredMethod"/>
        /// </summary>
        IPackage Package { get; }

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Set a named parameter value of the given type.
        /// </summary>
        /// <typeparam name="TParameterValue">The type of <see cref="IParameterValue"/> to set.</typeparam>
        /// <param name="name">The name of the parameter to set.</param>
        /// <param name="value">The parameter value.</param>
        void SetValue<TParameterValue>(string name, object value) where TParameterValue : IParameterValue;

        /// <summary>
        /// Set named parameter values of the given type.
        /// </summary>
        /// <typeparam name="TParameterValue">The type of <see cref="IParameterValue"/> to set.</typeparam>
        /// <param name="values">An dictionary of values to set.</param>
        void SetValues<TParameterValue>(IDictionary<string, object> values) where TParameterValue : IParameterValue;

        /// <summary>
        /// Gets parameter values of the given type.
        /// </summary>
        /// <typeparam name="TParameterValue">The type of <see cref="IParameterValue"/> to get.</typeparam>
        /// <param name="includeEmpty">Flag to include parameters with no value set.</param>
        /// <returns>A List of <see cref="TParameterValue"/>.</returns>
        IList<TParameterValue> GetValues<TParameterValue>(bool includeEmpty) where TParameterValue : IParameterValue;

        /// <summary>
        /// Gets a named parameter value of the given type.
        /// </summary>
        /// <typeparam name="TParameterValue">The type of <see cref="IParameterValue"/> to get.</typeparam>
        /// <param name="name">The name of the parameter to get.</param>
        /// <returns>The <see cref="TParameterValue"/></returns>
        TParameterValue GetValue<TParameterValue>(string name) where TParameterValue : IParameterValue;

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
        ExceptionDescription Handle(ExceptionDescription exceptionDescription, UnderlyingDatabaseException underlyingDatabaseException);
    }
}
