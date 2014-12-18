using Disposable.Common.ServiceLocator;
using Disposable.Test.Extensions;
using Disposable.MessagePipe.ServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.MessagePipe.ServiceLocator.Test
{
    [TestClass]
    public class MessagePipeServiceLocatorTest
    {
        public enum TestEnum
        {
            TestValue
        }

        public interface ITest
        {
            void Handler(IMessenger<TestEnum> messenger);
        }

        public class TestClass : ITest
        {
            public int Counter { get; private set; }
            
            public void Handler(IMessenger<TestEnum> messenger)
            {
                Counter++;
            }
        }
        
        [TestInitialize]
        public void Initialize()
        {
            Locator.Current.ResetRegsitrars();
        }
        
        [TestMethod]
        public void MessagePipe_DeliveryRouting_AsExpected()
        {
            // Arrange
            var testOne = new TestClass();
            var testTwo = new TestClass();

            var registrar = (Locator.Current as Locator).GetRegistrar();
            registrar.Register<ITest>(() => testOne.Counter > 1 ? testTwo : testOne);

            var messagePipe = new MessagePipe<TestEnum>(MessengerType.Stepping);

            messagePipe.Locator<TestEnum, ITest>(TestEnum.TestValue, x => x.Handler);

            var context = new MessageContext<TestEnum>(TestEnum.TestValue);

            // Act, Assert
            Assert.AreEqual(0, testOne.Counter);
            Assert.AreEqual(0, testTwo.Counter);

            messagePipe.Announce(context);
            Assert.AreEqual(1, testOne.Counter);
            Assert.AreEqual(0, testTwo.Counter);

            messagePipe.Announce(context);
            Assert.AreEqual(2, testOne.Counter);
            Assert.AreEqual(0, testTwo.Counter);

            messagePipe.Announce(context);
            Assert.AreEqual(2, testOne.Counter);
            Assert.AreEqual(1, testTwo.Counter);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceNotFoundException))]
        public void MessagePipe_DeliveryRoutingWithNoService_Throws()
        {
            var messagePipe = new MessagePipe<TestEnum>(MessengerType.Stepping);

            messagePipe.Locator<TestEnum, ITest>(TestEnum.TestValue, x => x.Handler);

            var context = new MessageContext<TestEnum>(TestEnum.TestValue);

            messagePipe.Announce(context);
        }
    }
}
