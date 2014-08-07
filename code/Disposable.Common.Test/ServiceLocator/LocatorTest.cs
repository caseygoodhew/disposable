using Disposable.Common.ServiceLocator;
using Disposable.Test;
using Disposable.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Disposable.Common.Test.ServiceLocator
{
    [TestClass]
    public class LocatorTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Locator.Current.ResetRegsitrars();
        }
        
        [TestMethod]
        public void Current_WithManyThreads_UsesSameInstance()
        {
            // Arrange, Act
            var locators = new List<ILocator>();
            MultiThreaded.Setup(100, () => locators.Add(Locator.Current));

            // give the threads time to execute
            var sleepCount = 0;
            while (sleepCount++ < 10 && locators.Any(x => x == null))
            {
                Thread.Sleep(5);
            }

            // Assert
            Assert.IsTrue(locators.Any());
            Assert.IsFalse(locators.Any(x => x == null));

            for (var i = 1; i < locators.Count; i++)
            {
                Assert.AreSame(locators[0], locators[i]);
            }
        }

        [TestMethod]
        public void Locator_WithRegisteredService_ReturnsSerivce()
        {
            var locator = Locator.Current as Locator;
            ILocator outLocator;
            object outObject;
            
            Assert.IsFalse(locator.TryGetInstance(out outLocator));
            Assert.IsFalse(locator.TryGetInstance(typeof(ILocator), out outObject));

            locator.Register<ILocator>(() => locator);

            Assert.AreSame(locator, locator.Instance<ILocator>());
            Assert.AreSame(locator, locator.Instance(typeof(ILocator)));
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceNotFoundException))]
        public void Locator_WithoutRegisteredService_ThrowsOnByGenericInstance()
        {
            Locator.Current.Instance<ILocator>();
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceNotFoundException))]
        public void Locator_WithoutRegisteredService_ThrowsOnByTypeInstance()
        {
            Locator.Current.Instance(typeof(ILocator));
        }

        [TestMethod]
        public void BaseRegistrar_WithMoqInterface_Succeeds()
        {
            // Arrange
            var testInterfaceMock = new Mock<ITestInterface>(MockBehavior.Loose);
            var locatorFunc = new Func<ITestInterface>(() => testInterfaceMock.Object);

            var locator = Locator.Current as Locator;

            // Act
            Assert.IsNotNull(locator);
            locator.BaseRegistrar.Register(locatorFunc);
            var result = Locator.Current.Instance<ITestInterface>();

            // Assert
            Assert.AreSame(testInterfaceMock.Object, result);
        }

        [TestMethod]
        public void OverrideRegistrar_WithMoqInterface_Succeeds()
        {
            // Arrange
            var baseTestInterfaceMock = new Mock<ITestInterface>(MockBehavior.Loose);
            var overrideTestInterfaceMock = new Mock<ITestInterface>(MockBehavior.Loose);
            var baseLocatorFunc = new Func<ITestInterface>(() => baseTestInterfaceMock.Object);
            var overrideLocatorFunc = new Func<ITestInterface>(() => overrideTestInterfaceMock.Object);

            var locator = Locator.Current as Locator;

            // Act
            Assert.IsNotNull(locator);
            locator.BaseRegistrar.Register(baseLocatorFunc);
            locator.Register(overrideLocatorFunc);
            var result = Locator.Current.Instance<ITestInterface>();

            // Assert
            Assert.AreSame(overrideTestInterfaceMock.Object, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceNotFoundException))]
        public void ResetRegistrars_WithBaseRegistration_Succeeds()
        {
            // Arrange
            var testInterfaceMock = new Mock<ITestInterface>(MockBehavior.Loose);
            var locatorFunc = new Func<ITestInterface>(() => testInterfaceMock.Object);

            var locator = Locator.Current as Locator;

            // Act, Assert
            Assert.IsNotNull(locator);
            locator.BaseRegistrar.Register(locatorFunc);
            var firstResult = Locator.Current.Instance<ITestInterface>();
            Assert.AreSame(testInterfaceMock.Object, firstResult);
            locator.ResetRegsitrars();

            Locator.Current.Instance<ITestInterface>();
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceNotFoundException))]
        public void ResetRegistrars_WithOverrideRegistration_Succeeds()
        {
            // Arrange
            var testInterfaceMock = new Mock<ITestInterface>(MockBehavior.Loose);
            var locatorFunc = new Func<ITestInterface>(() => testInterfaceMock.Object);

            var locator = Locator.Current as Locator;

            // Act, Assert
            Assert.IsNotNull(locator);
            locator.Register(locatorFunc);
            var firstResult = Locator.Current.Instance<ITestInterface>();
            Assert.AreSame(testInterfaceMock.Object, firstResult);
            locator.ResetRegsitrars();

            Locator.Current.Instance<ITestInterface>();
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceAlreadyRegisteredException))]
        public void BaseRegistrar_WithDuplicateRegistration_Throws()
        {
            // Arrange
            var testInterfaceMock = new Mock<ITestInterface>(MockBehavior.Loose);
            var locatorFunc = new Func<ITestInterface>(() => testInterfaceMock.Object);

            var locator = Locator.Current as Locator;

            // Act, Assert
            Assert.IsNotNull(locator);
            locator.BaseRegistrar.Register(locatorFunc);
            locator.BaseRegistrar.Register(locatorFunc);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceAlreadyRegisteredException))]
        public void OverrideRegistrar_WithDuplicateRegistration_Throws()
        {
            // Arrange
            var testInterfaceMock = new Mock<ITestInterface>(MockBehavior.Loose);
            var locatorFunc = new Func<ITestInterface>(() => testInterfaceMock.Object);

            var locator = Locator.Current as Locator;

            // Act, Assert
            Assert.IsNotNull(locator);
            locator.BaseRegistrar.Register(locatorFunc);
            locator.Register(locatorFunc);
            locator.Register(locatorFunc);
        }
    }

    public interface ITestInterface
    {
    }
}
