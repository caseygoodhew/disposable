namespace Disposable.Security.Policies
{
    /// <summary>
    /// Password policy framework
    /// </summary>
    public interface IPasswordPolicy
    {
        /// <summary>
        /// Gets the time window between which consecutive failed attempts to provide a valid password or password answer are tracked.
        /// </summary>
        int AttemptWindow { get; }

        /// <summary>
        /// Gets the maximum length allowed for a password.
        /// </summary>
        int MaxAllowableLength { get; }

        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        int MaxInvalidAttempts { get; }

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        int MinRequiredLength { get; }

        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        int MinRequiredNonAlphanumericCharacters { get; }
        
        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        string StrengthRegularExpression { get; }
    }
}
