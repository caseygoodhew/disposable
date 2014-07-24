using System;
using System.Data;
using System.Linq;

using Disposable.Common;
using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Attributes;
using Disposable.Data.Map.Binding;
using Disposable.Data.Map.DataSource;
using Disposable.Test.Common;
using Disposable.Test.Common.ServiceLocator;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Data.Map.Test.Binding
{
    [TestClass]
    public class TypeBindingTest
    {
        private class SomeClass
        {
            public int Property { get; set; }

            public int Field;

            [NoMap]
            public int BeginMapMethodCount { get; set; }

            [NoMap]
            public int EndMapMethodCount { get; set; }

            [BeforeMap]
            public void BeginMapMethod(IDataReader reader)
            {
                BeginMapMethodCount++;
            }

            [AfterMap]
            public void EndMapMethod(IDataReader reader)
            {
                EndMapMethodCount++;
            }
        }

        private class EmptyClassSuccess
        {
        }

        private class BeforeMapTestingSuccess
        {
            [BeforeMap]
            public void Method(IDataReader reader) { }
        }

        private class BeforeMapTestingFailReturnType
        {
            [BeforeMap]
            public bool Method(IDataReader reader)
            {
                return true;
            }
        }

        private class BeforeMapTestingFailParamCount
        {
            [BeforeMap]
            public void Method(IDataReader readerOne, IDataReader readerTwo) { }
        }

        private class BeforeMapTestingFailParamType
        {
            [BeforeMap]
            public void Method(int value) { }
        }

        private class AfterMapTestingSuccess
        {
            [AfterMap]
            public void Method(IDataReader reader) { }
        }

        private class AfterMapTestingFailReturnType
        {
            [AfterMap]
            public bool Method(IDataReader reader)
            {
                return true;
            }
        }

        private class AfterMapTestingFailParamCount
        {
            [AfterMap]
            public void Method(IDataReader readerOne, IDataReader readerTwo) { }
        }

        private class AfterMapTestingFailParamType
        {
            [AfterMap]
            public void Method(int value) { }
        }

        [TestInitialize]
        public void Initialize()
        {
            var locator = Locator.Current as Locator;
            locator.ResetRegsitrars();

            // ReSharper disable once PossibleNullReferenceException
            locator.Register<IMemberBindingFactory>(() => new MemberBindingFactory());
        }
        
        [TestMethod]
        public void TypeBinding_WithValidConstruction_FindsCorrectMembers()
        {
            var binding = new TypeBinding<SomeClass>();

            var members = binding.ToDictionary(x => x.Name);

            Assert.AreEqual(2, binding.Count());
            Assert.AreEqual(2, members.Count());
            Assert.IsTrue(members.ContainsKey("Property"));
            Assert.IsTrue(members.ContainsKey("Field"));
            Assert.IsInstanceOfType(members["Property"], typeof(PropertyBinding<SomeClass>));
            Assert.IsInstanceOfType(members["Field"], typeof(FieldBinding<SomeClass>));
        }

        [TestMethod]
        public void TypeBinding_WithValidConstruction_CallsBeforeAndAfterMappingMethods()
        {
            // Arranage
            IDataSourceReader reader = null;
            var obj = new SomeClass();
            
            // Validate test class assumptions
            Assert.AreEqual(0, obj.BeginMapMethodCount);
            Assert.AreEqual(0, obj.EndMapMethodCount);
           
            obj.BeginMapMethod(reader);
            Assert.AreEqual(1, obj.BeginMapMethodCount);
            Assert.AreEqual(0, obj.EndMapMethodCount);
           
            obj.EndMapMethod(reader);
            Assert.AreEqual(1, obj.BeginMapMethodCount);
            Assert.AreEqual(1, obj.EndMapMethodCount);

            // Act, Assert
            var binding = new TypeBinding<SomeClass>();

            binding.BeginMapping(obj, reader);
            Assert.AreEqual(2, obj.BeginMapMethodCount);
            Assert.AreEqual(1, obj.EndMapMethodCount);

            binding.EndMapping(obj, reader);
            Assert.AreEqual(2, obj.BeginMapMethodCount);
            Assert.AreEqual(2, obj.EndMapMethodCount);
        }

        [TestMethod]
        public void TypeBinding_BeforeMapTestingSuccess_Succeeds()
        {
            IDataSourceReader reader = null;
            var obj = new BeforeMapTestingSuccess();
            var binding = new TypeBinding<BeforeMapTestingSuccess>();
            binding.BeginMapping(obj, reader);
            binding.EndMapping(obj, reader);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TypeBinding_BeforeMapTestingFailParamCount_Throws()
        {
            new TypeBinding<BeforeMapTestingFailParamCount>();
        }


        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TypeBinding_BeforeMapTestingFailParamType_Throws()
        {
            new TypeBinding<BeforeMapTestingFailParamType>();
        }


        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TypeBinding_BeforeMapTestingFailReturnType_Throws()
        {
            new TypeBinding<BeforeMapTestingFailReturnType>();
        }

        [TestMethod]
        public void TypeBinding_AfterMapTestingSuccess_Succeeds()
        {
            IDataSourceReader reader = null;
            var obj = new AfterMapTestingSuccess();
            var binding = new TypeBinding<AfterMapTestingSuccess>();
            binding.BeginMapping(obj, reader);
            binding.EndMapping(obj, reader);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TypeBinding_AfterMapTestingFailParamCount_Throws()
        {
            new TypeBinding<AfterMapTestingFailParamCount>();
        }


        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TypeBinding_AfterMapTestingFailParamType_Throws()
        {
            new TypeBinding<AfterMapTestingFailParamType>();
        }


        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TypeBinding_AfterMapTestingFailReturnType_Throws()
        {
            new TypeBinding<AfterMapTestingFailReturnType>();
        }

        [TestMethod]
        public void TypeBinding_EmptyClassSuccess_Succeeds()
        {
            IDataSourceReader reader = null;
            var obj = new EmptyClassSuccess();
            var binding = new TypeBinding<EmptyClassSuccess>();
            binding.BeginMapping(obj, reader);
            binding.EndMapping(obj, reader);
        }

        [TestMethod]
        public void PartialTypeBinding_IEnumerableGetEnumerator_Succeeds()
        {
            var binding = new TypeBinding<EmptyClassSuccess>();
            binding.TestGetEnumerator();
        }
    }
}
