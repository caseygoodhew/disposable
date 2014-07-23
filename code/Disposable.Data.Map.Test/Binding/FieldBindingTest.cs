using System;
using System.Reflection;

using Disposable.Data.Map.Binding;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Data.Map.Test.Binding
{
    [TestClass]
    public class FieldBindingTest
    {
        private class SampleClass
        {
            public int PublicInt;

            internal int InternalInt;

            protected int ProtectedInt;

            private int PrivateInt;

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
        public void FieldBinding_AgainstPublicField_Succeeds()
        {
            // Arrange
            var value = 42;
            var sampleObj = new SampleClass();
            var fieldName = "PublicInt";
            var fieldType = typeof(int);

            // Act
            var binding = new FieldBinding<SampleClass>(typeof(SampleClass).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
            binding.SetValue(sampleObj, value);
            
            // Assert
            Assert.AreEqual(fieldName, binding.Name);
            Assert.AreEqual(fieldType, binding.DataType);
            Assert.AreEqual(value, sampleObj.PublicInt);
        }

        [TestMethod]
        public void FieldBinding_AgainstInternalField_Succeeds()
        {
            // Arrange
            var value = 42;
            var sampleObj = new SampleClass();
            var fieldName = "InternalInt";
            var fieldType = typeof(int);

            // Act
            var binding = new FieldBinding<SampleClass>(typeof(SampleClass).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
            binding.SetValue(sampleObj, value);

            // Assert
            Assert.AreEqual(fieldName, binding.Name);
            Assert.AreEqual(fieldType, binding.DataType);
            Assert.AreEqual(value, sampleObj.InternalInt);
        }

        [TestMethod]
        public void FieldBinding_AgainstProtectedField_Succeeds()
        {
            // Arrange
            var value = 42;
            var sampleObj = new SampleClass();
            var fieldName = "PrivateInt";
            var fieldType = typeof(int);

            // Act
            var binding = new FieldBinding<SampleClass>(typeof(SampleClass).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
            binding.SetValue(sampleObj, value);

            // Assert
            Assert.AreEqual(fieldName, binding.Name);
            Assert.AreEqual(fieldType, binding.DataType);
            Assert.AreEqual(value, sampleObj.GetPrivateInt());
        }

        [TestMethod]
        public void FieldBinding_AgainstPrivateField_Succeeds()
        {
            // Arrange
            var value = 42;
            var sampleObj = new SampleClass();
            var fieldName = "ProtectedInt";
            var fieldType = typeof(int);

            // Act
            var binding = new FieldBinding<SampleClass>(typeof(SampleClass).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
            binding.SetValue(sampleObj, value);

            // Assert
            Assert.AreEqual(fieldName, binding.Name);
            Assert.AreEqual(fieldType, binding.DataType);
            Assert.AreEqual(value, sampleObj.GetProtectedInt());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FieldBinding_WithNullFieldInfo_Throws()
        {
            new FieldBinding<SampleClass>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(TargetException))]
        public void FieldBinding_WithNullObjectOnSetValue_Throws()
        {
            var fieldName = "PublicInt";
            var value = 42;
            var binding = new FieldBinding<SampleClass>(typeof(SampleClass).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
            binding.SetValue(null, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FieldBinding_WithMismatchDataTypeOnSetValue_Throws()
        {
            var fieldName = "PublicInt";
            var value = "fails";
            var sampleObj = new SampleClass();
            var binding = new FieldBinding<SampleClass>(typeof(SampleClass).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
            binding.SetValue(sampleObj, value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PropertyBinding_WithMismatchGenericTypeToPropertyInfoDeclaringType_Throws()
        {
            var fieldName = "PublicInt";
            var sampleObj = new SampleClass();
            var binding = new FieldBinding<AnotherSampleClass>(typeof(SampleClass).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
        }
    }
}
