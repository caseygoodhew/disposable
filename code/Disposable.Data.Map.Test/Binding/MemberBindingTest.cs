using System;
using System.Reflection;

using Disposable.Data.Map.Binding;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Map.Test.Binding
{
    // Most member binding testing is perfomed indirectly via Field and Property Binding tests.
    [TestClass]
    public class MemberBindingTest
    {
        private class TestMemberBinding<T> : MemberBinding<T> where T : class
        {
            public TestMemberBinding(MemberInfo memberInfo) : base(memberInfo)
            {
            }

            public override Type DataType
            {
                get { throw new NotImplementedException(); }
            }

            public override void SetValue(T obj, object value)
            {
                throw new NotImplementedException();
            }
        }

        private class SomeClass { }
        
        [TestMethod]
        public void MemberBinding_Construction_Succeeds()
        {
            var memberInfo = new Mock<MemberInfo>();
            memberInfo.Setup(x => x.DeclaringType).Returns(() => typeof(SomeClass));
            var binding = new TestMemberBinding<SomeClass>(memberInfo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MemberBinding_ConstructionWithNullMemberInfo_Throws()
        {
            new TestMemberBinding<SomeClass>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MemberBinding_ConstructionWithNullDeclaringType_Throws()
        {
            var memberInfo = new Mock<MemberInfo>();
            memberInfo.Setup(x => x.DeclaringType).Returns(() => null);
            new TestMemberBinding<SomeClass>(memberInfo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MemberBinding_ConstructionWithMismatchDeclaringType_Throws()
        {
            var memberInfo = new Mock<MemberInfo>();
            memberInfo.Setup(x => x.DeclaringType).Returns(() => typeof(TestMemberBinding<>));
            var binding = new TestMemberBinding<SomeClass>(memberInfo.Object);
        }
    }
}
