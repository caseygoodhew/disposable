using System;

namespace Disposable.Data.Common.Exceptions
{
    /// <summary>
    /// Wrapper to rethrow any database exceptions which are unknown.
    /// </summary>
    public sealed class UnknownDatabaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownDatabaseException"/> class.
        /// </summary>
        /// <param name="innerException">The underlying exception.</param>
        public UnknownDatabaseException(Exception innerException)
            : base(@"Undefined Database Exception", innerException)
        {
        }
    }
}
