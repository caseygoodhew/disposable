using Disposable.Common.ServiceLocator;
using Disposable.Validation.Factory;
using FluentValidation.Mvc;

namespace Disposable.Web.Validation
{
    /// <summary>
    /// Static registration entry point for IOC registration
    /// </summary>
    public static class Registration
    {
        /// <summary>
        /// Static registration entry point for IOC registration
        /// </summary>
        /// <param name="locator">The locator</param>
        public static void Register(ILocator locator)
        {
            // TODO: Is this correct? (it's not using the locator)
            FluentValidationModelValidatorProvider.Configure(x => x.ValidatorFactory = new ValidationFactory());
        }
    }
}
