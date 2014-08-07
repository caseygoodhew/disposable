using System;

namespace Disposable.Data.Database.Exceptions
{
    /// <summary>
    /// Base exception for all programmatically defined custom database exceptions.
    /// </summary>
    [Serializable]
    public abstract class ProgrammaticDatabaseException : Exception
    {
    }
}
