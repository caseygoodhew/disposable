using System;
using Disposable.Common.ServiceLocator;
using FluentValidation;

namespace Disposable.Validation.Factory
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidationFactory : ValidatorFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validatorType"></param>
        /// <returns></returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            object validator;
            if (Locator.Current.TryGetInstance(validatorType, out validator))
            {
                return validator as IValidator;
            }

            return null;
        }
    }
}
