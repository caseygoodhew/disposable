using Disposable.Common.ServiceLocator;
using Disposable.Validation.Registration;
using Disposable.ViewModels.Registration;
using FluentValidation;

namespace Disposable.Validation
{
    public static class Validation
    {
        public static void Register(IRegistrar registrar)
        {
            registrar.Register<IValidator<RegistrationViewModel>>(() => new RegistrationViewModelValidator());
        }
    }
}
