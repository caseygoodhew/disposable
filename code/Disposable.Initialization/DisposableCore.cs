﻿using System;
using Disposable.Common.ServiceLocator;
using Disposable.Common.Services;
using Disposable.Validation;

namespace Disposable.Initialization
{
    /// <summary>
    /// Singleton to register all core services with the serivce locator
    /// </summary>
    public class DisposableCore
    {
        private static readonly Lazy<DisposableCore> DisposableCoreInstance = new Lazy<DisposableCore>(() => new DisposableCore());

        private DisposableCore()
        {
            var locator = (Locator.Current as Locator);
            
            if (locator == null)
            {
                throw new InvalidOperationException();
            }

            var registrar = locator.BaseRegistrar;

            
            Data.Access.Registration.Register(registrar);
            Data.ObjectMapping.Registration.Register(registrar);
            Data.Packages.Registration.Register(registrar);
            Data.Security.Registration.Register(registrar);
            
            Security.Registration.Register(registrar);
            
            Registration.Register(registrar);

            // misc
            registrar.Register<ITimeSource>(() => new LocalTimeSource());
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool Initialize()
        {
            return DisposableCoreInstance.Value != null;
        }
    }
}
