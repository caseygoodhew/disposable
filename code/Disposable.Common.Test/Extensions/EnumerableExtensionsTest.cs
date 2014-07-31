using System;
using System.Collections.Generic;
using System.Linq;

using Disposable.Common.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Common.Test.Extensions
{
    [TestClass]
    public class EnumerableExtensionsTest
    {
        [TestMethod]
        public void IsNullOrEmpty_WithNullEnumerable_ReturnsTrue()
        {
            IEnumerable<object> enumerable = null;
            Assert.IsTrue(enumerable.IsNullOrEmpty());
        }

        [TestMethod]
        public void IsNullOrEmpty_WithEmptyEnumerable_ReturnsTrue()
        {
            IEnumerable<object> enumerable = Enumerable.Empty<object>();
            Assert.IsTrue(enumerable.IsNullOrEmpty());
        }

        [TestMethod]
        public void IsNullOrEmpty_WithOneEntryInEnumerable_ReturnsFalse()
        {
            IEnumerable<object> enumerable = Enumerable.Repeat(new object(), 1);
            Assert.IsFalse(enumerable.IsNullOrEmpty());
            Assert.AreEqual(1, enumerable.Count());
        }

        [TestMethod]
        public void Concat_WithNullEnumerable_ReturnsEmptyString()
        {
            IEnumerable<object> enumerable = null;
            Assert.AreEqual(string.Empty, enumerable.Concat());
            Assert.AreEqual(string.Empty, enumerable.Concat("-"));
            Assert.AreEqual(string.Empty, enumerable.Concat(x => x.ToString().ToUpper()));
            Assert.AreEqual(string.Empty, enumerable.Concat(x => x.ToString().ToUpper(), "-"));
        }

        [TestMethod]
        public void Concat_WithEmptyEnumerable_ReturnsEmptyString()
        {
            IEnumerable<object> enumerable = Enumerable.Empty<object>();
            Assert.AreEqual(string.Empty, enumerable.Concat());
            Assert.AreEqual(string.Empty, enumerable.Concat("-"));
            Assert.AreEqual(string.Empty, enumerable.Concat(x => x.ToString().ToUpper()));
            Assert.AreEqual(string.Empty, enumerable.Concat(x => x.ToString().ToUpper(), "-"));
        }

        [TestMethod]
        public void IsNullOrEmpty_WithOneEntryInEnumerable_ReturnsExpected()
        {
            IEnumerable<object> enumerable = Enumerable.Repeat(new object(), 1);
            Assert.AreEqual("System.Object", enumerable.Concat());
            Assert.AreEqual("System.Object", enumerable.Concat("-"));
            Assert.AreEqual("SYSTEM.OBJECT", enumerable.Concat(x => x.ToString().ToUpper()));
            Assert.AreEqual("SYSTEM.OBJECT", enumerable.Concat(x => x.ToString().ToUpper()), "-");
            Assert.AreEqual(1, enumerable.Count());
        }

        [TestMethod]
        public void IsNullOrEmpty_WithTwoEntriesInEnumerable_ReturnsExpected()
        {
            IEnumerable<object> enumerable = Enumerable.Repeat(new object(), 2);
            Assert.AreEqual("System.ObjectSystem.Object", enumerable.Concat());
            Assert.AreEqual("System.Object-System.Object", enumerable.Concat("-"));
            Assert.AreEqual("SYSTEM.OBJECTSYSTEM.OBJECT", enumerable.Concat(x => x.ToString().ToUpper()));
            Assert.AreEqual("SYSTEM.OBJECT-SYSTEM.OBJECT", enumerable.Concat(x => x.ToString().ToUpper(), "-"));
            Assert.AreEqual(2, enumerable.Count());
        }
    }
}
