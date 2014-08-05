using System;
using System.Collections.Generic;
using System.Linq;
using Disposable.Data.Common.Exceptions;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Abstract class defining the common attributes required to call a stored method.
    /// This class should not be directly extended. Prefer <see cref="StoredProcedure"/> or <see cref="StoredFunction"/> instead.
    /// </summary>
    public abstract class StoredMethod : IStoredMethod
    {
        protected readonly IList<IInputParameter> InputParameters;
        
        protected readonly IList<IOutputParameter> OutputParameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredMethod"/> class.
        /// </summary>
        /// <param name="package">The <see cref="IPackage"/> that contains the procedure.</param>
        /// <param name="name">The name of the procedure.</param>
        /// <param name="parameters">The list of <see cref="IParameter"/>s in the package declaration.</param>
        protected StoredMethod(IPackage package, string name, params IParameter[] parameters)
        {
            Package = package;
            Name = name;
            InputParameters = parameters.OfType<IInputParameter>().ToList();
            OutputParameters = parameters.OfType<IOutputParameter>().ToList();

            if (InputParameters.Count + OutputParameters.Count != parameters.Length)
            {
                throw new ArgumentException("Unknown parameter types in parameters. IInputParameters.Count + IOutputParameters.Count <> IParameters.Count");
            }
        }

        /// <summary>
        /// Gets the <see cref="IPackage"/> which owns the <see cref="IStoredMethod"/>
        /// </summary>
        public IPackage Package { get; private set; }

        /// <summary>
        /// Gets the name of the procedure
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Instructs an <see cref="IStoredMethod"/> to handle a <see cref="ProgrammaticDatabaseException"/> which 
        /// was thrown in the database when the <see cref="IStoredMethodInstance"/> was invoked.
        /// </summary>
        /// <param name="storedMethodInstance">The <see cref="IStoredMethodInstance"/> that was running at the time of error generation.</param>
        /// <param name="exceptionDescription">The <see cref="ExceptionDescription"/> of the error to handle.</param>
        /// <param name="underlyingDatabaseException">The <see cref="UnderlyingDatabaseException"/></param>
        /// <returns>
        /// If the exception has been handled, this should be the same value that was passed into the method. 
        /// Returning any value other than this will result in an <see cref="UnhandledDatabaseException"/> being immediately thrown.
        /// </returns>
        public virtual ExceptionDescription Handle(
            IStoredMethodInstance storedMethodInstance,
            ExceptionDescription exceptionDescription,
            UnderlyingDatabaseException underlyingDatabaseException)
        {
            return ProgrammaticDatabaseExceptions.Unhandled;
        }

        /// <summary>
        /// Creates a new <see cref="IStoredMethodInstance"/>.
        /// </summary>
        /// <returns>A new <see cref="IStoredMethodInstance"/>.</returns>
        protected IStoredMethodInstance CreateInstance()
        {
            return new StoredMethodInstance(this, InputParameters, OutputParameters);
        }
    }
}
