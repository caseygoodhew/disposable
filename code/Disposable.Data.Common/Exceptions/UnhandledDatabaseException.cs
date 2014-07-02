using System;

namespace Disposable.Data.Common.Exceptions
{
    public sealed class UnhandledDatabaseException : Exception
    {
        public UnhandledDatabaseException(Exception innerException) : base(@"Unknown Database Exception", innerException)
        {
        }
    }
}
