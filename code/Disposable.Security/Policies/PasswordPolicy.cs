using System;

namespace Disposable.Security.Policies
{
    public class PasswordPolicy : IPasswordPolicy
    {
        private readonly static int MaxPasswordLength = 200;

        private readonly static int MinPasswordLength = 8;

        private readonly static string PasswordRegEx = @"^.*(?=.{6,})(?=.*\d).*$";
        
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
            get { throw new NotImplementedException(); }
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