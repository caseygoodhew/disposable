﻿using System;

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
        /// <param name="underlyingDatabaseException">The underlying exception.</param>
        public UnknownDatabaseException(UnderlyingDatabaseException underlyingDatabaseException) : base(@"Undefined Database Exception", underlyingDatabaseException)
        {
        }
    }
}
