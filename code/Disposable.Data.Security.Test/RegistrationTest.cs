using Disposable.Common.ServiceLocator;
using Disposable.Data.Security.Packages.User;
using Disposable.Test.Extensions;
using Disposable.Test.Runners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Disposable.Data.Security.Test
{
    [TestClass]
    public class RegistrationTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Locator.Current.ResetRegsitrars();
        }

        [TestMethod]
        public void VerifyRegisters()
        {
            RegistrationRunner.VerifyRegisters(
                new List<Type>(),
                Data.Registration.Register
            );
        }
    }
}
