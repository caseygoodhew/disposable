using Disposable.Common.ServiceLocator;
using Disposable.Validation.Factory;
using FluentValidation.Mvc;

namespace Disposable.Web.Validation
{
    public static class WebValidation
    {
        public static void Register(ILocator locator)
        {
            FluentValidationModelValidatorProvider.Configure(x => x.ValidatorFactory = new ValidationFactory());
        }
    }
}
