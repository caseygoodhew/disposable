using System;
using System.Runtime.Serialization;

namespace Disposable.Data.Security.Accounts.Exceptions
{
    /// <summary>
    /// Database exception indicating that the email address is invalid.
    /// </summary>
    [Serializable]
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

        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="StreamingContext"/>) for this serialization.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("email", Email);

            base.GetObjectData(info, context);
        }
    }
}
