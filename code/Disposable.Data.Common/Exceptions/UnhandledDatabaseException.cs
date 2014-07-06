using System;

namespace Disposable.Data.Common.Exceptions
{
    /// <summary>
    /// Wrapper to rethrow any <see cref="ProgrammaticDatabaseException"/> exceptions which are not handled by the IStoredMethod
    /// </summary>
    public sealed class UnhandledDatabaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnhandledDatabaseException"/> class.
        /// </summary>
        /// <param name="underlyingDatabaseException">The underlying exception.</param>
        public UnhandledDatabaseException(UnderlyingDatabaseException underlyingDatabaseException) : base(@"Unhandled Database Exception", underlyingDatabaseException)
        {
        }
    }
}
