using System;

namespace Disposable.Data.Security.Accounts.Exceptions
{
    /// <summary>
    /// Database exception indicating that this password has been recently used list.
    /// </summary>
    [Serializable]
    public class DuplicatePasswordException : AccountException
    {
    }
}
