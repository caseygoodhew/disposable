using System;
using System.Collections.Generic;
using System.Linq;

using Disposable.Common.Extensions;
using Disposable.Test.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.MessagePipe.Test
{
    [TestClass]
    public class MessengerTest
    {
        private enum TestEnum
        {
            ValueOne,
            ValueTwo
        }

        private class TestMessenger : Messenger<TestEnum>
        {
            public TestMessenger(IList<Action<IMessenger<TestEnum>>> handlers, MessageContext<TestEnum> messageContext)
                : base(handlers, messageContext)
            {
            }
        }

        private class TestPhantomMessenger : Messenger<GenericEnumPhantom>
        {
            public TestPhantomMessenger(IList<Action<IMessenger<GenericEnumPhantom>>> handlers, MessageContext<GenericEnumPhantom> messageContext)
                : base(handlers, messageContext)
            {
            }
        }

        private class TestMessageContext : MessageContext<TestEnum>
        {
            public int Counter { get; set; }

            public TestMessageContext(TestEnum messageType)
                : base(messageType)
            {
            }
        }

        private static void MessageHandler(IMessenger<TestEnum> messenger)
        {
            var context = messenger.GetContext<TestMessageContext>(EnumExtensions.GetValues<TestEnum>());
            context.Counter++;
        }

        [TestMethod]
        public void Messenger_WithNoHandlers_DoesNotThrow()
        {
            // Arrange
            var handlers = Enumerable.Empty<Action<IMessenger<TestEnum>>>().ToList();
            var messageContext = new TestMessageContext(TestEnum.ValueOne);

            // act, assert
            var messenger = new TestMessenger(handlers, messageContext);
            messenger.Forward();
            messenger.Forward();
        }

        [TestMethod]
        public void Messenger_WithExtraForward_ForwardsExpectedNumberOfTimes()
        {
            // Arrange
            var handlers = new List<Action<IMessenger<TestEnum>>> { MessageHandler, MessageHandler, MessageHandler };
            var messageContext = new TestMessageContext(TestEnum.ValueOne);

            // act
            var messenger = new TestMessenger(handlers, messageContext);
            
            // assert
            Assert.AreEqual(3, handlers.Count);

            Assert.AreEqual(0, messageContext.Counter);
            messenger.Forward();
            Assert.AreEqual(1, messageContext.Counter);
            messenger.Forward();
            Assert.AreEqual(2, messageContext.Counter);
            messenger.Forward();
            Assert.AreEqual(3, messageContext.Counter);
            messenger.Forward();
            Assert.AreEqual(3, messageContext.Counter);
            messenger.Forward();
            Assert.AreEqual(3, messageContext.Counter);
        }

        [TestMethod]
        public void Messenger_GetContext_ReturnSameObject()
        {
            // Arrange
            var handlers = Enumerable.Empty<Action<IMessenger<TestEnum>>>().ToList();
            var messageContext = new TestMessageContext(TestEnum.ValueOne);
            var messenger = new TestMessenger(handlers, messageContext);

            // Act
            var defaultTyping = messenger.GetContext(EnumExtensions.GetValues<TestEnum>());
            var genericTyping = messenger.GetContext<TestMessageContext>(EnumExtensions.GetValues<TestEnum>());

            // Assert
            Assert.AreSame(messageContext, defaultTyping);
            Assert.AreSame(defaultTyping, genericTyping);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Messenger_WithGenericEnumPhantom_Throws()
        {
            // Arrange, Act, Assert
            var messenger = new TestPhantomMessenger(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Messenger_WithNullHandlers_Throws()
        {
            // Arrange, Act, Assert
            IList<Action<IMessenger<TestEnum>>> handlers = null;
            var messageContext = new TestMessageContext(TestEnum.ValueOne);
            var messenger = new TestMessenger(handlers, messageContext);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Messenger_WithNullContext_Throws()
        {
            // Arrange, Act, Assert
            var handlers = Enumerable.Empty<Action<IMessenger<TestEnum>>>().ToList();
            TestMessageContext messageContext = null;
            var messenger = new TestMessenger(handlers, messageContext);
        }

        [TestMethod]
        public void Messenger_GetContextWithVariousMessageTypes_BehavesAsExpected()
        {
            // Arrange, Act, Assert
            AssertDoesNotThrowInvalidOperationException(x => x.GetContext(TestEnum.ValueOne));
            AssertThrowsInvalidOperationException(x => x.GetContext(TestEnum.ValueTwo));

            AssertDoesNotThrowInvalidOperationException(x => x.GetContext<TestMessageContext>(TestEnum.ValueOne));
            AssertThrowsInvalidOperationException(x => x.GetContext<TestMessageContext>(TestEnum.ValueTwo));

            AssertDoesNotThrowInvalidOperationException(x => x.GetContext(TestEnum.ValueOne, TestEnum.ValueTwo));
            AssertDoesNotThrowInvalidOperationException(x => x.GetContext<TestMessageContext>(TestEnum.ValueOne, TestEnum.ValueTwo));

            AssertDoesNotThrowInvalidOperationException(x => x.GetContext(new[] { TestEnum.ValueOne }));
            AssertThrowsInvalidOperationException(x => x.GetContext(new[] { TestEnum.ValueTwo }));
            
            AssertDoesNotThrowInvalidOperationException(x => x.GetContext<TestMessageContext>(new[] { TestEnum.ValueOne }));
            AssertThrowsInvalidOperationException(x => x.GetContext<TestMessageContext>(new[] { TestEnum.ValueTwo }));
            
            AssertDoesNotThrowInvalidOperationException(x => x.GetContext(new[] { TestEnum.ValueOne, TestEnum.ValueTwo }));
            AssertDoesNotThrowInvalidOperationException(x => x.GetContext<TestMessageContext>(new[] { TestEnum.ValueOne, TestEnum.ValueTwo }));

            AssertThrowsInvalidOperationException(x => x.GetContext(new TestEnum[] { }));
            AssertThrowsInvalidOperationException(x => x.GetContext<TestMessageContext>(new TestEnum[] { }));
        }

        private static void AssertThrowsInvalidOperationException(Action<TestMessenger> toTest)
        {
            Assert.IsTrue(ThrowsInvalidOperationException(toTest));
        }

        private static void AssertDoesNotThrowInvalidOperationException(Action<TestMessenger> toTest)
        {
            Assert.IsFalse(ThrowsInvalidOperationException(toTest));
        }
        
        private static bool ThrowsInvalidOperationException(Action<TestMessenger> toTest)
        {
            var handlers = Enumerable.Empty<Action<IMessenger<TestEnum>>>().ToList();
            var messageContext = new TestMessageContext(TestEnum.ValueOne);
            var messenger = new TestMessenger(handlers, messageContext);

            try
            {
                toTest.Invoke(messenger);
            }
            catch (InvalidOperationException)
            {
                return true;
            }

            return false;
        }
    }
}