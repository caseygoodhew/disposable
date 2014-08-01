using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using Disposable.Data.Map.Binding;
using Disposable.Data.Map.DataSource;
using Disposable.Test.Runners;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Map.Test.Binding
{
    [TestClass]
    public class PartialTypeBindingTest
    {
        public class SomeClass { }

        private Mock<ITypeBinding<SomeClass>> sourceTypeBinding;

        private Mock<IDataSourceReader> dataSourceReader;
        
        [TestInitialize]
        public void Initialize()
        {
            var names = new List<string> { "ItemOne", "ItemTwo", "ItemThree" };
            var memberBindings = names.Select(x => CreateMockMemberBinding(x).Object);
            
            sourceTypeBinding = new Mock<ITypeBinding<SomeClass>>(MockBehavior.Strict);
            sourceTypeBinding.Setup(x => x.GetEnumerator()).Returns(memberBindings.GetEnumerator);

            var columns = new List<string> { "Mismatch", names.First(), names.Skip(1).First() };
            dataSourceReader = new Mock<IDataSourceReader>();
            dataSourceReader.Setup(x => x.HasOrdinal(It.IsAny<string>())).Returns<string>(columns.Contains);
        }

        [TestMethod]
        public void PartialTypeBinding_Construction_Succeeds()
        {
            var binding = new PartialTypeBinding<SomeClass>(sourceTypeBinding.Object, dataSourceReader.Object);
            var members = binding.ToDictionary(x => x.Name);

            Assert.AreEqual(2, members.Count);
            Assert.IsTrue(members.ContainsKey("ItemOne"));
            Assert.IsTrue(members.ContainsKey("ItemTwo"));
        }

        [TestMethod]
        public void PartialTypeBinding_BeginMapping_CallsSourceTypeBinding()
        {
            sourceTypeBinding.Setup(x => x.BeginMapping(It.IsAny<SomeClass>(), It.IsAny<IDataSourceReader>()));

            var binding = new PartialTypeBinding<SomeClass>(sourceTypeBinding.Object, dataSourceReader.Object);
            binding.BeginMapping(null, dataSourceReader.Object);

            sourceTypeBinding.Verify(x => x.BeginMapping(It.IsAny<SomeClass>(), It.IsAny<IDataSourceReader>()), Times.Once);
        }

        [TestMethod]
        public void PartialTypeBinding_EndMapping_CallsSourceTypeBinding()
        {
            sourceTypeBinding.Setup(x => x.EndMapping(It.IsAny<SomeClass>(), It.IsAny<IDataSourceReader>()));

            var binding = new PartialTypeBinding<SomeClass>(sourceTypeBinding.Object, dataSourceReader.Object);
            binding.EndMapping(null, dataSourceReader.Object);

            sourceTypeBinding.Verify(x => x.EndMapping(It.IsAny<SomeClass>(), It.IsAny<IDataSourceReader>()), Times.Once);
        }

        [TestMethod]
        public void PartialTypeBinding_IEnumerableGetEnumerator_Succeeds()
        {
            var binding = new PartialTypeBinding<SomeClass>(sourceTypeBinding.Object, dataSourceReader.Object);
            EnumeratorRunner.GetEnumerator(binding);
        }

        private static Mock<IMemberBinding<SomeClass>> CreateMockMemberBinding(string name)
        {
            var result = new Mock<IMemberBinding<SomeClass>>();
            result.Setup(x => x.Name).Returns(() => name);
            return result;
        }
    }
}
