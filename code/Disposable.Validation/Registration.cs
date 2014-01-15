using Disposable.Common.ServiceLocator;
using Disposable.Validation.UserAccounts;
using Disposable.ViewModels.UserAccounts;
using FluentValidation;

namespace Disposable.Validation
{
    public static class Registration
    {
        public static void Register(IRegistrar registrar)
        {
            registrar.Register<IValidator<RegistrationViewModel>>(() => new RegistrationViewModelValidator());
        }
    }
}
