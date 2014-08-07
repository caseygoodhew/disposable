using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Disposable.Text.Test
{
    [TestClass]
    public class WordTest
    {
        [TestMethod]
        public void ValidConstruction_WhenGettingValueUpperLowerProperLength_ReturnsExpected()
        {
            // Arrange
            var value = "caSey";
            var lower = "casey";
            var upper = "CASEY";
            var proper = "Casey";
            var length = 5;

            // Act
            var word = new Word(value);

            // Assert
            Assert.AreEqual(value, word.Value);
            Assert.AreEqual(lower, word.Lower);
            Assert.AreEqual(upper, word.Upper);
            Assert.AreEqual(proper, word.Proper);
            Assert.AreEqual(length, word.Length);
        }

        [TestMethod]
        public void Subword_WithValidRange_ReturnsCorrectValueFromCache()
        {
            // Arrange
            var value = "caSey";
            
            // Act
            var word = new Word(value);
            var subWord1A = word.Sub(2);
            var subWord1B = word.Sub(2);
            var subWord2A = word.Sub(2, 2);
            var subWord2B = word.Sub(2, 2);

            // Assert
            Assert.AreEqual("Sey", subWord1A.Value);
            Assert.AreSame(subWord1A, subWord1B);
            Assert.AreEqual("Se", subWord2A.Value);
            Assert.AreSame(subWord2A, subWord2B);
        }

        [TestMethod]
        public void Indexing_WithValidIndicies_ReturnsCorrectCharacter()
        {
            // Arrange
            var value = "caSey";

            // Act
            var word = new Word(value);
            
            // Assert
            Assert.AreEqual('c', word[0]);
            Assert.AreEqual('a', word[1]);
            Assert.AreEqual('S', word[2]);
            Assert.AreEqual('e', word[3]);
            Assert.AreEqual('y', word[4]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidConstruction_WithEmptyValue_Throws()
        {
            new Word(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidConstruction_WithNullValue_Throws()
        {
            new Word(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidConstruction_WithWhitespaceValue_Throws()
        {
            new Word(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Subword_WithNegativeStart_Throws()
        {
            var value = "caSey";
            var word = new Word(value);
            word.Sub(-1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Subword_WithStartLongerThanCharactersInValue_Throws()
        {
            var value = "caSey";
            var word = new Word(value);
            word.Sub(word.Length, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Subword_WithWithZeroLength_Throws()
        {
            var value = "caSey";
            var word = new Word(value);
            word.Sub(0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Subword_WithLengthExceedingStartPlusValueLength_Throws()
        {
            var value = "caSey";
            var word = new Word(value);
            word.Sub(0, word.Length + 1);
        }
    }
}
