
using Disposable.Common.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Common.Test.Extensions
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void ToTitleCase_WithNullString_ReturnsNullString()
        {
            string str = null;
            Assert.IsNull(str.ToTitleCase());
        }

        [TestMethod]
        public void ToTitleCase_WithEmptyString_ReturnsEmptyString()
        {
            string str = string.Empty;
            Assert.AreEqual(string.Empty, str.ToTitleCase());
        }

        [TestMethod]
        public void ToTitleCase_WithSingleSpace_ReturnsSingleSpace()
        {
            string str = " ";
            Assert.AreEqual(" ", str.ToTitleCase());
        }

        [TestMethod]
        public void ToTitleCase_WithLowerWord_ReturnsTitleWord()
        {
            string str = "casey";
            Assert.AreEqual("Casey", str.ToTitleCase());
        }

        [TestMethod]
        public void ToTitleCase_WithUpperWord_ReturnsTitleWord()
        {
            string str = "CASEY";
            Assert.AreEqual("Casey", str.ToTitleCase());
        }

        [TestMethod]
        public void ToTitleCase_WithMultipleWords_ReturnsTitleWords()
        {
            Assert.AreEqual("Casey Goodhew", "casey goodhew".ToTitleCase());
            Assert.AreEqual(" Casey Goodhew", " casey goodhew".ToTitleCase());
            Assert.AreEqual(" Casey Goodhew ", " casey goodhew ".ToTitleCase());
            Assert.AreEqual("Casey Goodhew ", "casey goodhew ".ToTitleCase());
        }
    }
}
