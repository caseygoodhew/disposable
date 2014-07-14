﻿using System;
using Disposable.Data.Common.Exceptions;

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
