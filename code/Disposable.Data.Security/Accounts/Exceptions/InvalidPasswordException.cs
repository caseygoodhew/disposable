using System;

namespace Disposable.Data.Security.Accounts.Exceptions
{
    /// <summary>
    /// Database exception indicating that the password does not meet the minimum password strength requirements.
    /// </summary>
    [Serializable]
    public class InvalidPasswordException : AccountException
    {
    }
}
