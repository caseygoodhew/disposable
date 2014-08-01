using System;
using System.Collections.Generic;

using Disposable.Common.ServiceLocator;
using Disposable.Data.Packages.User;
using Disposable.Test.Extensions;
using Disposable.Test.Runners;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Data.Packages.Test
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
                new List<Type>
                    {
                        typeof(IUserPackage),
                    },
                Registration.Register
            );
        }
    }
}
