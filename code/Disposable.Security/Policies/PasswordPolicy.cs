using System;

namespace Disposable.Security.Policies
{
    /// <summary>
    /// A user password policy.
    /// </summary>
    public class PasswordPolicy : IPasswordPolicy
    {
        private static readonly int MaxPasswordLength = 200;

        private static readonly int MinPasswordLength = 8;

        private static readonly int MinSpecialCharcters = 2;

        private static readonly string PasswordRegEx = @"^.*(?=.{8,})(?=.*\d)(?=.*\d).*$";
        
        /// <summary>
        /// Gets the time window between which consecutive failed attempts to provide a valid password or password answer are tracked.
        /// </summary>
        public int AttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the maximum length allowed for a password.
        /// </summary>
        public int MaxAllowableLength
        {
            get { return MaxPasswordLength; }
        }

        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        public int MaxInvalidAttempts
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        public int MinRequiredLength
        {
            get { return MinPasswordLength; }
        }

        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        public int MinRequiredNonAlphanumericCharacters
        {
            get { return MinSpecialCharcters; }
        }

        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        public string StrengthRegularExpression
        {
            get
            {
                return PasswordRegEx;
            }
        }
    }
}