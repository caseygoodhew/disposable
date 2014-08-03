using Disposable.Data.Common.Exceptions;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Interface defining the common attributes required to call a stored method.
    /// </summary>
    public interface IStoredMethod
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
        ExceptionDescription Handle(IStoredMethodInstance storedMethodInstance, ExceptionDescription exceptionDescription, UnderlyingDatabaseException underlyingDatabaseException);
    }
}
