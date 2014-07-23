using System;
using System.Reflection;

using Disposable.Data.Map.Binding;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Data.Map.Test.Binding
{
    [TestClass]
    public class PropertyBindingTest
    {
        private class SampleClass
        {
            public int PublicInt { get; set; }

            internal int InternalInt { get; set; }

            protected int ProtectedInt { get; set; }

            private int PrivateInt { get; set; }

            public int GetProtectedInt()
            {
                return ProtectedInt;
            }

            public int GetPrivateInt()
            {
                return PrivateInt;
            }
        }

        private class AnotherSampleClass { }
        
        [TestMethod]
        public void PropertyBinding_AgainstPublicProperty_Succeeds()
        {
            // Arrange
            var value = 42;
            var sampleObj = new SampleClass();
            var propertyName = "PublicInt";
            var propertyType = typeof(int);

            // Act
            var binding = new PropertyBinding<SampleClass>(typeof(SampleClass).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
            binding.SetValue(sampleObj, value);
            
            // Assert
            Assert.AreEqual(propertyName, binding.Name);
            Assert.AreEqual(propertyType, binding.DataType);
            Assert.AreEqual(value, sampleObj.PublicInt);
        }

        [TestMethod]
        public void PropertyBinding_AgainstInternalProperty_Succeeds()
        {
            // Arrange
            var value = 42;
            var sampleObj = new SampleClass();
            var propertyName = "InternalInt";
            var propertyType = typeof(int);

            // Act
            var binding = new PropertyBinding<SampleClass>(typeof(SampleClass).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
            binding.SetValue(sampleObj, value);

            // Assert
            Assert.AreEqual(propertyName, binding.Name);
            Assert.AreEqual(propertyType, binding.DataType);
            Assert.AreEqual(value, sampleObj.InternalInt);
        }

        [TestMethod]
        public void PropertyBinding_AgainstProtectedProperty_Succeeds()
        {
            // Arrange
            var value = 42;
            var sampleObj = new SampleClass();
            var propertyName = "PrivateInt";
            var propertyType = typeof(int);

            // Act
            var binding = new PropertyBinding<SampleClass>(typeof(SampleClass).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
            binding.SetValue(sampleObj, value);

            // Assert
            Assert.AreEqual(propertyName, binding.Name);
            Assert.AreEqual(propertyType, binding.DataType);
            Assert.AreEqual(value, sampleObj.GetPrivateInt());
        }

        [TestMethod]
        public void PropertyBinding_AgainstPrivateProperty_Succeeds()
        {
            // Arrange
            var value = 42;
            var sampleObj = new SampleClass();
            var propertyName = "ProtectedInt";
            var propertyType = typeof(int);

            // Act
            var binding = new PropertyBinding<SampleClass>(typeof(SampleClass).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
            binding.SetValue(sampleObj, value);

            // Assert
            Assert.AreEqual(propertyName, binding.Name);
            Assert.AreEqual(propertyType, binding.DataType);
            Assert.AreEqual(value, sampleObj.GetProtectedInt());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PropertyBinding_WithNullPropertyInfo_Throws()
        {
            new PropertyBinding<SampleClass>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(TargetException))]
        public void PropertyBinding_WithNullObjectOnSetValue_Throws()
        {
            var propertyName = "PublicInt";
            var value = 42;
            var binding = new PropertyBinding<SampleClass>(typeof(SampleClass).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
            binding.SetValue(null, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PropertyBinding_WithMismatchDataTypeOnSetValue_Throws()
        {
            var propertyName = "PublicInt";
            var value = "fails";
            var sampleObj = new SampleClass();
            var binding = new PropertyBinding<SampleClass>(typeof(SampleClass).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
            binding.SetValue(sampleObj, value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PropertyBinding_WithMismatchGenericTypeToPropertyInfoDeclaringType_Throws()
        {
            var propertyName = "PublicInt";
            var sampleObj = new SampleClass();
            var binding = new PropertyBinding<AnotherSampleClass>(typeof(SampleClass).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
        }
    }
}
