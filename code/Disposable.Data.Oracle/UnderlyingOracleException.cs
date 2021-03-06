﻿using System;

using Disposable.Data.Database.Exceptions;

using Oracle.DataAccess.Client;

namespace Disposable.Data.Oracle
{
    /// <summary>
    /// Wrapper for <see cref="OracleException"/>s to bind them into the core data access stack.
    /// </summary>
    [Serializable]
    internal class UnderlyingOracleException : UnderlyingDatabaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnderlyingOracleException"/> class.
        /// </summary>
        /// <param name="oracleException">The underlying <see cref="OracleException"/>.</param>
        internal UnderlyingOracleException(OracleException oracleException) : base(oracleException)
        {
        }
    }
}
