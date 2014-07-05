namespace Disposable.Data.Security.Accounts.Exceptions
{
    /// <summary>
    /// Database exception indicating that this email address is already registered.
    /// </summary>
    public class DuplicateEmailException : AccountException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEmailException"/> class.
        /// </summary>
        /// <param name="email">The email address that has been duplicated.</param>
        public DuplicateEmailException(string email)
        {
            Email = email;
        }

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public string Email { get; private set; }
    }
}
