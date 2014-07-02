using System;

namespace Disposable.Data.Common.Exceptions
{
    public sealed class UnknownDatabaseException : Exception
    {
        public UnknownDatabaseException(Exception innerException) : base(@"Undefined Database Exception", innerException)
        {
        }
    }
}
