using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Disposable.Common.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Common.Test.Extensions
{
    [TestClass]
    public class TypeExtensionsTest
    {
        [TestMethod]
        public void IsEnumerable_WithDefaultFalseStrict_Succeeds()
        {
            var param = typeof(TypeExtensions).GetMethod("IsIEnumerable").GetParameters().Skip(1).First();
            Assert.AreEqual("strict", param.Name);
            Assert.AreEqual(typeof(bool), param.ParameterType);
            Assert.AreEqual(false, param.DefaultValue);
            
            Assert.IsTrue(typeof(IEnumerable<>).IsIEnumerable());
            Assert.IsTrue(typeof(IEnumerable<bool>).IsIEnumerable());
            Assert.IsTrue(typeof(IEnumerable<TypeExtensionsTest>).IsIEnumerable());

            Assert.IsTrue(typeof(IList<>).IsIEnumerable());
            Assert.IsTrue(typeof(IList<bool>).IsIEnumerable());
            Assert.IsTrue(typeof(IList<TypeExtensionsTest>).IsIEnumerable());

            Assert.IsTrue(typeof(List<>).IsIEnumerable());
            Assert.IsTrue(typeof(List<bool>).IsIEnumerable());
            Assert.IsTrue(typeof(List<TypeExtensionsTest>).IsIEnumerable());

            Assert.IsFalse(typeof(bool).IsIEnumerable(true));
            Assert.IsFalse(typeof(TypeExtensionsTest).IsIEnumerable(true));
        }

        [TestMethod]
        public void IsEnumerable_WithTrueStrict_Succeeds()
        {
            Assert.IsTrue(typeof(IEnumerable<>).IsIEnumerable(true));
            Assert.IsTrue(typeof(IEnumerable<bool>).IsIEnumerable(true));
            Assert.IsTrue(typeof(IEnumerable<TypeExtensionsTest>).IsIEnumerable(true));

            Assert.IsFalse(typeof(IList<>).IsIEnumerable(true));
            Assert.IsFalse(typeof(IList<bool>).IsIEnumerable(true));
            Assert.IsFalse(typeof(IList<TypeExtensionsTest>).IsIEnumerable(true));

            Assert.IsFalse(typeof(List<>).IsIEnumerable(true));
            Assert.IsFalse(typeof(List<bool>).IsIEnumerable(true));
            Assert.IsFalse(typeof(List<TypeExtensionsTest>).IsIEnumerable(true));

            Assert.IsFalse(typeof(bool).IsIEnumerable(true));
            Assert.IsFalse(typeof(TypeExtensionsTest).IsIEnumerable(true));
        }

        public class SysDefaultConstructor
        {
        }

        public class DefaultConstructor
        {
            public DefaultConstructor()
            {
            }
        }

        public class PrivateDefaultConstructor
        {
            private PrivateDefaultConstructor()
            {
            }
        }

        public class ConstructorWithParameter
        {
            public ConstructorWithParameter(int test)
            {
            }
        }

        public class MixedConstructors
        {
            public MixedConstructors()
            {
            }
            
            public MixedConstructors(int test)
            {
            }
        }

        [TestMethod]
        public void HasDefaultConstructor_WithVarious_Succeeds()
        {
            Assert.IsTrue(typeof(SysDefaultConstructor).HasDefaultConstructor());
            Assert.IsTrue(typeof(DefaultConstructor).HasDefaultConstructor());
            Assert.IsFalse(typeof(PrivateDefaultConstructor).HasDefaultConstructor());
            Assert.IsTrue(typeof(PrivateDefaultConstructor).HasDefaultConstructor(BindingFlags.NonPublic));
            Assert.IsFalse(typeof(ConstructorWithParameter).HasDefaultConstructor());
            Assert.IsTrue(typeof(MixedConstructors).HasDefaultConstructor());
        }
    }
}
