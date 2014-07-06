using System;

namespace Disposable.Data.Common.Exceptions
{
    /// <summary>
    /// Wrapper for database specific exceptions to bind them into the core data access stack.
    /// </summary>
    public abstract class UnderlyingDatabaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnderlyingDatabaseException"/> class.
        /// </summary>
        /// <param name="exception">The underlying database exception.</param>
        protected UnderlyingDatabaseException(Exception exception) : base("Underlying database exception", exception)
        {
        }
    }
}