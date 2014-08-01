using System;

using Disposable.Common.ServiceLocator;
using Disposable.Data.Packages.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Packages.Test.Core
{
    [TestClass]
    public class PackageTest
    {
        private class PackageTester : Package
        {
            public PackageTester(string schema, string name)
                : base(schema, name)
            {
            }

            public void AddTest<TStoredMethod>(Func<TStoredMethod> locatorFunc) where TStoredMethod : class, IStoredMethod
            {
                Add(locatorFunc);
            }

            public TStoredMethod GetTest<TStoredMethod>() where TStoredMethod : class, IStoredMethod
            {
                return Get<TStoredMethod>();
            }
        }
        
        [TestMethod]
        public void Package_Constructs_AsExpected()
        {
            var schema = "Casey";
            var name = "Goodhew";
            var storedMethodMock = new Mock<IStoredMethod>();
            
            var package = new PackageTester(schema, name);
            
            Assert.AreEqual(schema, package.Schema);
            Assert.AreEqual(name, package.Name);

            package.AddTest(() => storedMethodMock.Object);
            Assert.AreSame(storedMethodMock.Object, package.GetTest<IStoredMethod>());
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceAlreadyRegisteredException))]
        public void Package_AddDuplicateStoredMethod_Throws()
        {
            var schema = "Casey";
            var name = "Goodhew";
            var storedMethodMock = new Mock<IStoredMethod>();

            var package = new PackageTester(schema, name);

            package.AddTest(() => storedMethodMock.Object);
            package.AddTest(() => storedMethodMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceNotFoundException))]
        public void Package_GetUnRegisteredStoredMethod_Throws()
        {
            var schema = "Casey";
            var name = "Goodhew";
            
            var package = new PackageTester(schema, name);

            package.GetTest<IStoredMethod>();
        }
    }
}
