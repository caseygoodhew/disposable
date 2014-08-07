using Disposable.Data.Database.Exceptions;
using System;

namespace Disposable.Data.Security.Accounts.Exceptions
{
    /// <summary>
    /// Abstract database account exception
    /// </summary>
    [Serializable]
    public abstract class AccountException : ProgrammaticDatabaseException
    {
    }
}
