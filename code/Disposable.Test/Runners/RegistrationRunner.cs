using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Disposable.Common.ServiceLocator;
using Disposable.Test.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Test.Runners
{
    internal static class RegistrationRunner
    {
        public static void VerifyRegisters(IEnumerable<Type> types, Action<IRegistrar> invokeRegistration)
        {
            var locator = Locator.Current as Locator;
            var registrar = locator.GetRegistrar();

            var typeList = types.ToList();
            typeList.ForEach(x => Assert.IsFalse(registrar.IsRegistered(x)));

            invokeRegistration(registrar);

            typeList.ForEach(x => Assert.IsTrue(registrar.IsRegistered(x)));
            typeList.ForEach(x => Assert.IsNotNull(locator.Instance(x)));
        }
    }
}
