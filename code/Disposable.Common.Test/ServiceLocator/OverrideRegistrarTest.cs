
using Disposable.Common.ServiceLocator;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Common.Test.ServiceLocator
{
    [TestClass]
    public class OverrideRegistrarTest
    {
        public interface ITestClass { }

        public class TestClassOne : ITestClass { }

        public class TestClassTwo : ITestClass { }

        [TestMethod]
        public void Registrar_WithValidUse_Succeeds()
        {
            RegistrarTestBase.Registrar_WithValidUse_Succeeds(new OverrideRegistrar(new BaseRegistrar()));
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceAlreadyRegisteredException))]
        public void Register_WithDuplicationRegistrations_Throws()
        {
            RegistrarTestBase.Register_WithDuplicationRegistrations_Throws(new OverrideRegistrar(new BaseRegistrar()));
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceNotFoundException))]
        public void Instance_OfUnregisteredGenericType_Throws()
        {
            RegistrarTestBase.Instance_OfUnregisteredGenericType_Throws(new OverrideRegistrar(new BaseRegistrar()));
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceNotFoundException))]
        public void Instance_OfUnregisteredType_Throws()
        {
            RegistrarTestBase.Instance_OfUnregisteredGenericType_Throws(new OverrideRegistrar(new BaseRegistrar()));
        }

        [TestMethod]
        public void Instance_DoesNot_CacheFunc()
        {
            RegistrarTestBase.Instance_DoesNot_CacheFunc(new OverrideRegistrar(new BaseRegistrar()));
        }

        [TestMethod]
        public void OverrideRegistrar_Overrides_BaseRegistrations()
        {
            var baseRegistrar = new BaseRegistrar();
            var overrideRegistrar = new OverrideRegistrar(baseRegistrar);

            baseRegistrar.Register<ITestClass>(() => new TestClassOne());

            Assert.IsInstanceOfType(baseRegistrar.Instance<ITestClass>(), typeof(TestClassOne));
            Assert.IsInstanceOfType(overrideRegistrar.Instance<ITestClass>(), typeof(TestClassOne));

            overrideRegistrar.Register<ITestClass>(() => new TestClassTwo());

            Assert.IsInstanceOfType(baseRegistrar.Instance<ITestClass>(), typeof(TestClassOne));
            Assert.IsInstanceOfType(overrideRegistrar.Instance<ITestClass>(), typeof(TestClassTwo));
        }

    }
}
