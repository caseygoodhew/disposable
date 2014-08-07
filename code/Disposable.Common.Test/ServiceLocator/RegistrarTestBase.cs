using Disposable.Common.ServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Disposable.Common.Test.ServiceLocator
{
    public static class RegistrarTestBase
    {
        public interface INothing { }
        
        public interface ITestClass { }
        
        public class TestClass : ITestClass { }
        
        public static void Registrar_WithValidUse_Succeeds(BaseRegistrar registrar)
        {
            var testClass = new TestClass();

            registrar.Register<ITestClass>(() => testClass);
            registrar.Register(() => testClass);

            Assert.IsTrue(registrar.IsRegistered<ITestClass>());
            Assert.IsTrue(registrar.IsRegistered<TestClass>());

            Assert.IsTrue(registrar.IsRegistered(typeof(ITestClass)));
            Assert.IsTrue(registrar.IsRegistered(typeof(TestClass)));

            Assert.AreSame(testClass, registrar.Instance<ITestClass>());
            Assert.AreSame(testClass, registrar.Instance<TestClass>());

            Assert.AreSame(testClass, registrar.Instance(typeof(ITestClass)));
            Assert.AreSame(testClass, registrar.Instance(typeof(TestClass)));

            ITestClass interfaceResult;
            TestClass classResult;
            object objectResult;
            
            Assert.IsTrue(registrar.TryGetInstance(out interfaceResult));
            Assert.AreSame(testClass, interfaceResult);

            Assert.IsTrue(registrar.TryGetInstance(out classResult));
            Assert.AreSame(testClass, classResult);

            Assert.IsTrue(registrar.TryGetInstance(typeof(ITestClass), out objectResult));
            Assert.AreSame(testClass, objectResult);

            Assert.IsTrue(registrar.TryGetInstance(typeof(TestClass), out objectResult));
            Assert.AreSame(testClass, objectResult);

            INothing nothingResult;

            Assert.IsFalse(registrar.TryGetInstance(out nothingResult));
            Assert.IsFalse(registrar.TryGetInstance(typeof(INothing), out objectResult));
        }

        public static void Register_WithDuplicationRegistrations_Throws(BaseRegistrar registrar)
        {
            registrar.Register<ITestClass>(() => new TestClass());
            registrar.Register<ITestClass>(() => new TestClass());
        }

        public static void Instance_OfUnregisteredGenericType_Throws(BaseRegistrar registrar)
        {
            registrar.Instance<INothing>();
        }

        public static void Instance_OfUnregisteredType_Throws(BaseRegistrar registrar)
        {
            registrar.Instance(typeof(INothing));
        }

        public static void Instance_DoesNot_CacheFunc(BaseRegistrar registrar)
        {
            var callCounter = 0;
            var func = new Func<ITestClass>(
                () =>
                    {
                        callCounter++;
                        return new TestClass();
                    });

            registrar.Register(func);
            
            var resultOne = registrar.Instance<ITestClass>();
            Assert.AreEqual(1, callCounter);
            
            var resultTwo = registrar.Instance<ITestClass>();
            Assert.AreEqual(2, callCounter);
            
            Assert.AreNotSame(resultOne, resultTwo);
        }
    }
}
