using System;

namespace Disposable.Data.Database.Exceptions
{
    /// <summary>
    /// Wrapper to rethrow any database exceptions which are unknown.
    /// </summary>
    [Serializable]
    public sealed class UnknownDatabaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownDatabaseException"/> class.
        /// </summary>
        /// <param name="underlyingDatabaseException">The underlying exception.</param>
        public UnknownDatabaseException(UnderlyingDatabaseException underlyingDatabaseException) : base(@"Undefined Database Exception", underlyingDatabaseException)
        {
        }
    }
}
