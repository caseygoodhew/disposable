﻿using Disposable.Common.ServiceLocator;
using FluentValidation;
using System;

namespace Disposable.Validation.Factory
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidatorFactory : ValidatorFactoryBase
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
