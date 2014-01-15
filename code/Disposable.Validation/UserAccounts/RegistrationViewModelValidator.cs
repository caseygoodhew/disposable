using System;
using System.Data;
using Disposable.Common.ServiceLocator;
using Disposable.Security.Policies;
using Disposable.ViewModels.UserAccounts;
using FluentValidation;

namespace Disposable.Validation.UserAccounts
{
    /// <summary>
    /// 
    /// </summary>
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        private readonly Lazy<IPasswordPolicy> _passwordPolicy = new Lazy<IPasswordPolicy>(() => Locator.Current.Instance<IPasswordPolicy>());

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationViewModelValidator"/> class.
        /// </summary>
        public RegistrationViewModelValidator()
        {
            var policy = _passwordPolicy.Value;
            
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            // TODO: make this: https://www.microsoft.com/security/pc-security/password-checker.aspx
            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(policy.MinRequiredLength, policy.MaxAllowableLength)
                .WithMessage(
                    @"{PropertyName} must be at least {0} characters long", 
                    x => policy.MinRequiredLength)
                .Matches(policy.StrengthRegularExpression)
                .WithMessage(
                    string.Format(
                        @"{{PropertyName}} must contain {0} characters with {1} numbers or symbols",
                        policy.MinRequiredLength, 
                        policy.MinRequiredNonAlphanumericCharacters));
            
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }
    }
}
