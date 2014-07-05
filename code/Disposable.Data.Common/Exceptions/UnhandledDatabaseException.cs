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
        /// <param name="innerException">The underlying exception.</param>
        public UnhandledDatabaseException(Exception innerException) : base(@"Unhandled Database Exception", innerException)
        {
        }
    }
}
