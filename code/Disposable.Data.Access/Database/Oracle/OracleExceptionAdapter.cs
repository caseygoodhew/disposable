using System;
using Disposable.Data.Common.Exceptions;
using Disposable.Data.Packages.Core;
using Oracle.DataAccess.Client;

namespace Disposable.Data.Access.Database.Oracle
{
    internal static class OracleExceptionAdapter
    {
        internal static void Throw(OracleException oracleException, IStoredMethod storedMethod)
        {
            var oe = (OracleExceptions)Enum.ToObject(typeof(OracleExceptions), oracleException.Number);

            switch (oe)
            {
                case OracleExceptions.DuplicateEmail:
                    storedMethod.Throw(ProgrammaticDatabaseExceptions.DuplicateEmail, oracleException);
                    break;

                default:
                    throw new UnknownDatabaseException(oracleException);
            }

            throw new UnhandledDatabaseException(oracleException);
        }
    }
}
