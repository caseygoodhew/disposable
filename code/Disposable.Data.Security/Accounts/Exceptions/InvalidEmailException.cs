namespace Disposable.Data.Security.Accounts.Exceptions
{
    /// <summary>
    /// Database exception indicating that the email address is invalid.
    /// </summary>
    public class InvalidEmailException : AccountException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEmailException"/> class.
        /// </summary>
        /// <param name="email">The email address that has been duplicated.</param>
        public InvalidEmailException(string email)
        {
            Email = email;
        }

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public string Email { get; private set; }
    }
}
