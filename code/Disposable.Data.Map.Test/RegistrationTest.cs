using System;
using System.Data;

using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Binding;
using Disposable.Data.Map.DataSource;
using Disposable.Test.Common.ServiceLocator;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Map.Test
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
        public void Registration_Registers_AllExpected()
        {
            var registrar = new Mock<IRegistrar>(MockBehavior.Strict);

            // order is important here -> IDataSourceReader extends IDataReader so much be setup first.
            // this is an issue with Mock 
            // https://code.google.com/p/moq/issues/detail?id=377
            registrar.Setup(x => x.Register(It.IsAny<Func<IDataSourceMapper<IDataSourceReader>>>()));
            registrar.Setup(x => x.Register(It.IsAny<Func<IDataSourceMapper<IDataReader>>>()));
            registrar.Setup(x => x.Register(It.IsAny<Func<IDataSourceMapper<DataSet>>>()));
            registrar.Setup(x => x.Register(It.IsAny<Func<IMemberBindingFactory>>()));
            registrar.Setup(x => x.Register(It.IsAny<Func<ITypeBindingFactory>>()));
            
            Registration.Register(registrar.Object);
            
            registrar.VerifyAll();
        }

        [TestMethod]
        public void Registration_AllowsLocation_Invocation()
        {
            var locator = Locator.Current as Locator;
            var registrar = locator.GetRegistrar() as IRegistrar;

            Assert.IsFalse(registrar.IsRegistered<ITypeBindingFactory>());
            Assert.IsFalse(registrar.IsRegistered<IMemberBindingFactory>());
            Assert.IsFalse(registrar.IsRegistered<IDataSourceMapper<DataSet>>());
            Assert.IsFalse(registrar.IsRegistered<IDataSourceMapper<IDataReader>>());
            Assert.IsFalse(registrar.IsRegistered<IDataSourceMapper<IDataSourceReader>>());
            
            Registration.Register(registrar);

            Assert.IsTrue(registrar.IsRegistered<ITypeBindingFactory>());
            Assert.IsTrue(registrar.IsRegistered<IMemberBindingFactory>());
            Assert.IsTrue(registrar.IsRegistered<IDataSourceMapper<DataSet>>());
            Assert.IsTrue(registrar.IsRegistered<IDataSourceMapper<IDataReader>>());
            Assert.IsTrue(registrar.IsRegistered<IDataSourceMapper<IDataSourceReader>>());

            Assert.IsNotNull(locator.Instance<ITypeBindingFactory>());
            Assert.IsNotNull(locator.Instance<IMemberBindingFactory>());
            Assert.IsNotNull(locator.Instance<IDataSourceMapper<DataSet>>());
            Assert.IsNotNull(locator.Instance<IDataSourceMapper<IDataReader>>());
            Assert.IsNotNull(locator.Instance<IDataSourceMapper<IDataSourceReader>>());
        }
    }
}
