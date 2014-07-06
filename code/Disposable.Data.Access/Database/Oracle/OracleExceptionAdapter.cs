using System;
using Disposable.Data.Common.Exceptions;
using Disposable.Data.Packages.Core;
using Oracle.DataAccess.Client;

namespace Disposable.Data.Access.Database.Oracle
{
    /// <summary>
    /// Handles converting custom oracle errors to .net exceptions.
    /// </summary>
    internal static class OracleExceptionAdapter
    {
        /// <summary>
        /// Relay method to convert and rethrow a custom oracle exceptions to an <see cref="IStoredMethod"/>
        /// </summary>
        /// <param name="oracleException">The <see cref="OracleException"/> to rethrow.</param>
        /// <param name="storedMethod">The <see cref="IStoredMethod"/> which invoked the <see cref="OracleException"/>.</param>
        internal static void Throw(OracleException oracleException, IStoredMethod storedMethod)
        {
            var underlyingException = new UnderlyingOracleException(oracleException);

            var oe = (OracleExceptions)Enum.ToObject(typeof(OracleExceptions), oracleException.Number);
            var isHandled = false;

            switch (oe)
            {
                case OracleExceptions.DuplicateEmail:
                    isHandled = storedMethod.Handle(ProgrammaticDatabaseExceptions.DuplicateEmail, underlyingException) == ProgrammaticDatabaseExceptions.DuplicateEmail;
                    break;

                default:
                    throw new UnknownDatabaseException(underlyingException);
            }

            if (!isHandled)
            {
                throw new UnhandledDatabaseException(underlyingException);
            }
        }
    }
}
