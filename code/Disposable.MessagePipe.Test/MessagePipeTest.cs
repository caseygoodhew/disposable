using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.MessagePipe.Test
{
    [TestClass]
    public class MessagePipeTest
    {
        private enum TestEnum
        {
            ValueOne,
            ValueTwo,
            ValueThree
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

        private static void BlockingMessageHandler(IMessanger<TestEnum> messanger)
        {
            var context = messanger.GetContext<TestMessageContext>();
            context.ActionList.Add("Block");
        }

        [TestMethod]
        public void MessasgePipe_WithMultipleRegistrations_Handles()
        {
            // arrange
            var messagePipe = new MessagePipe<TestEnum>();
            messagePipe.Register(TestEnum.ValueOne, ForwardingMessageHandler);
            messagePipe.Register(TestEnum.ValueOne, ForwardingMessageHandler);
            messagePipe.Register(TestEnum.ValueOne, ForwardingMessageHandler);

            messagePipe.Register(TestEnum.ValueTwo, ForwardingMessageHandler);
            messagePipe.Register(TestEnum.ValueTwo, BlockingMessageHandler);
            messagePipe.Register(TestEnum.ValueTwo, ForwardingMessageHandler);

            var messageContextOne = new TestMessageContext(TestEnum.ValueOne);
            var messageContextTwo = new TestMessageContext(TestEnum.ValueTwo);
            var messageContextThree = new TestMessageContext(TestEnum.ValueThree);

            // act
            messagePipe.Announce(messageContextOne);
            messagePipe.Announce(messageContextTwo);
            messagePipe.Announce(messageContextThree);

            // assert
            Assert.AreEqual(3, messageContextOne.ActionList.Count);
            Assert.AreEqual(2, messageContextTwo.ActionList.Count);
            Assert.AreEqual(0, messageContextThree.ActionList.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MessasgePipe_WithGenericEnumPhantom_Throws()
        {
            // Arrange, Act, Assert
            var messagePipe = new MessagePipe<GenericEnumPhantom>();
        }

        
    }
}
