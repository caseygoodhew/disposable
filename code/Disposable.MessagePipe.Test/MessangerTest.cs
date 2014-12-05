using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.MessagePipe.Test
{
    [TestClass]
    public class MessangerTest
    {
        private enum TestEnum
        {
            ValueOne
        }

        private class TestMessageContext : MessageContext<TestEnum>
        {
            public readonly List<string> ActionList = new List<string>();
            
            public TestMessageContext(TestEnum messageType)
                : base(messageType)
            {
            }
        }

        private static void ForwardingMessageHandler(IMessanger<TestEnum> messanger)
        {
            var context = messanger.GetContext<TestMessageContext>();
            context.ActionList.Add("Forward");
            
            messanger.Forward();
        }

        private void BlockingMessageHandler(IMessanger<TestEnum> messanger)
        {
            var context = messanger.GetContext<TestMessageContext>();
            context.ActionList.Add("Block");
        }
        
        [TestMethod]
        public void Messanger_WithNoHandlers_DoesNotThrow()
        {
            // Arrange
            var handlers = Enumerable.Empty<Action<IMessanger<TestEnum>>>().ToList();
            var messageContext = new TestMessageContext(TestEnum.ValueOne);
            
            // act, assert
            var messanger = new Messanger<TestEnum>(handlers, messageContext);
            messanger.Forward();
            messanger.Forward();
        }

        [TestMethod]
        public void Messanger_WithContinuousForward_Forwards()
        {
            // Arrange
            var handlers = new List<Action<IMessanger<TestEnum>>> { ForwardingMessageHandler, ForwardingMessageHandler, ForwardingMessageHandler };
            var messageContext = new TestMessageContext(TestEnum.ValueOne);

            // act
            var messanger = new Messanger<TestEnum>(handlers, messageContext);
            messanger.Forward();
            
            // assert
            Assert.AreEqual(handlers.Count, messageContext.ActionList.Count);
        }

        [TestMethod]
        public void Messanger_WithExtraForward_ForwardsExpectedNumberOfTimes()
        {
            // Arrange
            var handlers = new List<Action<IMessanger<TestEnum>>> { ForwardingMessageHandler, ForwardingMessageHandler, ForwardingMessageHandler };
            var messageContext = new TestMessageContext(TestEnum.ValueOne);

            // act
            var messanger = new Messanger<TestEnum>(handlers, messageContext);
            messanger.Forward();
            messanger.Forward();

            // assert
            Assert.AreEqual(handlers.Count, messageContext.ActionList.Count);
        }

        [TestMethod]
        public void Messanger_WithBlocker_ForwardsExpectedNumberOfTimes()
        {
            // Arrange
            var handlers = new List<Action<IMessanger<TestEnum>>> { ForwardingMessageHandler, BlockingMessageHandler, ForwardingMessageHandler };
            var messageContext = new TestMessageContext(TestEnum.ValueOne);

            // act
            var messanger = new Messanger<TestEnum>(handlers, messageContext);
            messanger.Forward();

            // assert
            Assert.AreEqual(2, messageContext.ActionList.Count);
        }

        [TestMethod]
        public void Messanger_GetContext_ReturnSameObject()
        {
            // Arrange
            var handlers = Enumerable.Empty<Action<IMessanger<TestEnum>>>().ToList();
            var messageContext = new TestMessageContext(TestEnum.ValueOne);
            var messanger = new Messanger<TestEnum>(handlers, messageContext);

            // Act
            var defaultTyping = messanger.GetContext();
            var genericTyping = messanger.GetContext<TestMessageContext>();

            // Assert
            Assert.AreSame(messageContext, defaultTyping);
            Assert.AreSame(defaultTyping, genericTyping);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Messanger_WithGenericEnumPhantom_Throws()
        {
            // Arrange, Act, Assert
            var messanger = new Messanger<GenericEnumPhantom>(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Messanger_WithNullHandlers_Throws()
        {
            // Arrange, Act, Assert
            IList<Action<IMessanger<TestEnum>>> handlers = null;
            var messageContext = new TestMessageContext(TestEnum.ValueOne);
            var messanger = new Messanger<TestEnum>(handlers, messageContext);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Messanger_WithNullContext_Throws()
        {
            // Arrange, Act, Assert
            var handlers = Enumerable.Empty<Action<IMessanger<TestEnum>>>().ToList();
            TestMessageContext messageContext = null;
            var messanger = new Messanger<TestEnum>(handlers, messageContext);
        }
    }
}