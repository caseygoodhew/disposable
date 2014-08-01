using System;

using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Binding;
using Disposable.Data.Map.DataSource;
using Disposable.Test.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Map.Test.Binding
{
    [TestClass]
    public class TypeBindingFactoryTest
    {
        private class SomeClass { }
        
        [TestInitialize]
        public void Initialize()
        {
            var locator = Locator.Current as Locator;
            locator.ResetRegsitrars();

            locator.Register<IMemberBindingFactory>(() => new MemberBindingFactory());

        }

        [TestMethod]
        public void TypeBindingFactory_Get_Succeeds()
        {
            var reader = new Mock<IDataSourceReader>();
            
            var factory = new TypeBindingFactory();
            Assert.IsNotNull(factory.Get<SomeClass>());
            Assert.IsNotNull(factory.Get<SomeClass>(reader.Object));
        }

        [TestMethod]
        public void TypeBindingFactory_Get_RegistersWithLocator()
        {
            var factory = new TypeBindingFactory();
            ITypeBinding<SomeClass> obj;

            Assert.IsFalse(Locator.Current.TryGetInstance(out obj));
            factory.Get<SomeClass>();
            Assert.IsTrue(Locator.Current.TryGetInstance(out obj));
        }

        [TestMethod]
        public void TypeBindingFactory_Get_UsesLocator()
        {
            var factory = new TypeBindingFactory();
            ITypeBinding<SomeClass> obj = null;
            (Locator.Current as Locator).Register(() => obj);

            var result = factory.Get<SomeClass>();
            Assert.AreSame(obj, result);
        }
    }
}
