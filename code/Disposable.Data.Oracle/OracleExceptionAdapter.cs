using System;

using Disposable.Data.Database.Exceptions;
using Disposable.Data.Packages;

using Oracle.DataAccess.Client;

namespace Disposable.Data.Oracle
{
    /// <summary>
    /// Handles converting custom oracle errors to .net exceptions.
    /// </summary>
    internal static class OracleExceptionAdapter
    {
        /// <summary>
        /// Relay method to convert and rethrow a custom oracle exceptions to an <see cref="IStoredMethodInstance"/>
        /// </summary>
        /// <param name="oracleException">The <see cref="OracleException"/> to rethrow.</param>
        /// <param name="storedMethodInstance">The <see cref="IStoredMethodInstance"/> which invoked the <see cref="OracleException"/>.</param>
        internal static void Throw(OracleException oracleException, IStoredMethodInstance storedMethodInstance)
        {
            var underlyingException = new UnderlyingOracleException(oracleException);

            var oe = (OracleExceptions)Enum.ToObject(typeof(OracleExceptions), oracleException.Number);
            var isHandled = false;

            switch (oe)
            {
                case OracleExceptions.DuplicateEmail:
                    isHandled = storedMethodInstance.Handle(ProgrammaticDatabaseExceptions.DuplicateEmail, underlyingException) == ProgrammaticDatabaseExceptions.DuplicateEmail;
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
