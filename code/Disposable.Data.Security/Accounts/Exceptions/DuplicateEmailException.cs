namespace Disposable.Data.Security.Accounts.Exceptions
{
    public class DuplicateEmailException : AccountException
    {
        public string Email { get; private set; }

        public DuplicateEmailException(string email)
        {
            Email = email;
        }
    }
}
