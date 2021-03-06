﻿using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.MessagePipe.Test
{
    [TestClass]
    public class MessageContextTest
    {
        private enum TestEnum
        {
            ValueOne,
            ValueTwo
        }

        [TestMethod]
        public void MessageContext_Constructs_AsExpected()
        {
            // arrange, act
            var context = new MessageContext<TestEnum>(TestEnum.ValueOne);
            var testValue = new MessageContextTest();
            context.Dictionary["test"] = testValue;

            // assert
            Assert.AreEqual(TestEnum.ValueOne, context.MessageType);
            Assert.AreSame(testValue, context.Dictionary["test"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MessageContext_WithGenericEnumPhantom_Throws()
        {
            // Arrange, Act, Assert
            var value = new GenericEnumPhantom();
            var messageContext = new MessageContext<GenericEnumPhantom>(value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MessageContext_WithNullMessageType_Throws()
        {
            // Arrange, Act, Assert
            var context = new MessageContext<TestEnum?>(null);
        }

        [TestMethod]
        public void MessageContext_ValidIsMessageType_ReturnsTrue()
        {
            // Arrange, Act, Assert
            var context = new MessageContext<TestEnum>(TestEnum.ValueOne);
            Assert.IsTrue(context.Is(TestEnum.ValueOne));
        }

        [TestMethod]
        public void MessageContext_InvalidIsMessageType_ReturnsFalse()
        {
            // Arrange, Act, Assert
            var context = new MessageContext<TestEnum>(TestEnum.ValueOne);
            Assert.IsFalse(context.Is(TestEnum.ValueTwo));
        }
    }
}
