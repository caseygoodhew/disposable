using Disposable.Common.ServiceLocator;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Common.Test.ServiceLocator
{
    [TestClass]
    public class BaseRegistrarTest
    {
        [TestMethod]
        public void Registrar_WithValidUse_Succeeds()
        {
            RegistrarTestBase.Registrar_WithValidUse_Succeeds(new BaseRegistrar());
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceAlreadyRegisteredException))]
        public void Register_WithDuplicationRegistrations_Throws()
        {
            RegistrarTestBase.Register_WithDuplicationRegistrations_Throws(new BaseRegistrar());
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceNotFoundException))]
        public void Instance_OfUnregisteredGenericType_Throws()
        {
            RegistrarTestBase.Instance_OfUnregisteredGenericType_Throws(new BaseRegistrar());
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceNotFoundException))]
        public void Instance_OfUnregisteredType_Throws()
        {
            RegistrarTestBase.Instance_OfUnregisteredGenericType_Throws(new BaseRegistrar());
        }

        [TestMethod]
        public void Instance_DoesNot_CacheFunc()
        {
            RegistrarTestBase.Instance_DoesNot_CacheFunc(new BaseRegistrar());
        }
    }
}
