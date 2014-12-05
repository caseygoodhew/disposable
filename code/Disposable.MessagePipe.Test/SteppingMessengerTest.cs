using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.MessagePipe.Test
{
    [TestClass]
    public class SteppingMessengerTest
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

        private static void ForwardingMessageHandler(IMessenger<TestEnum> messenger)
        {
            var context = messenger.GetContext<TestMessageContext>();
            context.ActionList.Add("Forward");
            
            messenger.Forward();
        }

        private void BlockingMessageHandler(IMessenger<TestEnum> messenger)
        {
            var context = messenger.GetContext<TestMessageContext>();
            context.ActionList.Add("Block");
        }
        
        [TestMethod]
        public void Messenger_WithNoHandlers_DoesNotThrow()
        {
            // Arrange
            var handlers = Enumerable.Empty<Action<IMessenger<TestEnum>>>().ToList();
            var messageContext = new TestMessageContext(TestEnum.ValueOne);
            
            // act, assert
            var messenger = new SteppingMessenger<TestEnum>(handlers, messageContext);
            messenger.Forward();
            messenger.Forward();
        }

        [TestMethod]
        public void Messenger_WithContinuousForward_Forwards()
        {
            // Arrange
            var handlers = new List<Action<IMessenger<TestEnum>>> { ForwardingMessageHandler, ForwardingMessageHandler, ForwardingMessageHandler };
            var messageContext = new TestMessageContext(TestEnum.ValueOne);

            // act
            var messenger = new SteppingMessenger<TestEnum>(handlers, messageContext);
            messenger.Forward();
            
            // assert
            Assert.AreEqual(handlers.Count, messageContext.ActionList.Count);
        }

        [TestMethod]
        public void Messenger_WithExtraForward_ForwardsExpectedNumberOfTimes()
        {
            // Arrange
            var handlers = new List<Action<IMessenger<TestEnum>>> { ForwardingMessageHandler, ForwardingMessageHandler, ForwardingMessageHandler };
            var messageContext = new TestMessageContext(TestEnum.ValueOne);

            // act
            var messenger = new SteppingMessenger<TestEnum>(handlers, messageContext);
            messenger.Forward();
            messenger.Forward();

            // assert
            Assert.AreEqual(handlers.Count, messageContext.ActionList.Count);
        }

        [TestMethod]
        public void Messenger_WithBlocker_ForwardsExpectedNumberOfTimes()
        {
            // Arrange
            var handlers = new List<Action<IMessenger<TestEnum>>> { ForwardingMessageHandler, BlockingMessageHandler, ForwardingMessageHandler };
            var messageContext = new TestMessageContext(TestEnum.ValueOne);

            // act
            var messenger = new SteppingMessenger<TestEnum>(handlers, messageContext);
            messenger.Forward();

            // assert
            Assert.AreEqual(2, messageContext.ActionList.Count);
        }

        [TestMethod]
        public void Messenger_GetContext_ReturnSameObject()
        {
            // Arrange
            var handlers = Enumerable.Empty<Action<IMessenger<TestEnum>>>().ToList();
            var messageContext = new TestMessageContext(TestEnum.ValueOne);
            var messenger = new SteppingMessenger<TestEnum>(handlers, messageContext);

            // Act
            var defaultTyping = messenger.GetContext();
            var genericTyping = messenger.GetContext<TestMessageContext>();

            // Assert
            Assert.AreSame(messageContext, defaultTyping);
            Assert.AreSame(defaultTyping, genericTyping);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Messenger_WithGenericEnumPhantom_Throws()
        {
            // Arrange, Act, Assert
            var messenger = new SteppingMessenger<GenericEnumPhantom>(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Messenger_WithNullHandlers_Throws()
        {
            // Arrange, Act, Assert
            IList<Action<IMessenger<TestEnum>>> handlers = null;
            var messageContext = new TestMessageContext(TestEnum.ValueOne);
            var messenger = new SteppingMessenger<TestEnum>(handlers, messageContext);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Messenger_WithNullContext_Throws()
        {
            // Arrange, Act, Assert
            var handlers = Enumerable.Empty<Action<IMessenger<TestEnum>>>().ToList();
            TestMessageContext messageContext = null;
            var messenger = new SteppingMessenger<TestEnum>(handlers, messageContext);
        }
    }
}