using Disposable.Data.Map.Binding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace Disposable.Data.Map.Test.Binding
{
    [TestClass]
    public class MemberBindingFactoryTest
    {
        public class SampleClass
        {
            public int Field;

            public int Property { get; set; }
        }

        [TestMethod]
        public void GetBinding_WithFieldInfo_ReturnsFieldBinding()
        {
            var type = typeof(SampleClass);
            var factory = new MemberBindingFactory();

            var binding = factory.Get<SampleClass>(type.GetField("Field"));

            Assert.IsInstanceOfType(binding, typeof(FieldBinding<SampleClass>));
        }

        [TestMethod]
        public void GetBinding_WithPropertyInfo_ReturnsPropertyBinding()
        {
            var type = typeof(SampleClass);
            var factory = new MemberBindingFactory();

            var binding = factory.Get<SampleClass>(type.GetProperty("Property"));

            Assert.IsInstanceOfType(binding, typeof(PropertyBinding<SampleClass>));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetBinding_WithMemberInfo_Throws()
        {
            MemberInfo memberInfo = null;
            var factory = new MemberBindingFactory();

            var binding = factory.Get<SampleClass>(memberInfo);

            Assert.IsInstanceOfType(binding, typeof(PropertyBinding<>));
        }
    }
}
