using System;
using System.Data;
using Disposable.Common.ServiceLocator;
using Disposable.Security.Policies;
using Disposable.ViewModels.Registration;
using FluentValidation;

namespace Disposable.Validation.Registration
{
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        private readonly Lazy<IPasswordPolicy> _passwordPolicy = new Lazy<IPasswordPolicy>(() => Locator.Current.Instance<IPasswordPolicy>()); 
        
        public RegistrationViewModelValidator()
        {
            var policy = _passwordPolicy.Value;
            
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Length(policy.MinRequiredLength, policy.MaxAllowableLength).Matches(policy.StrengthRegularExpression);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }
    }
}
