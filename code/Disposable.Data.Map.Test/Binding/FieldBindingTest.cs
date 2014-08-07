using Disposable.Data.Map.Attributes;
using Disposable.Data.Map.Binding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace Disposable.Data.Map.Test.Binding
{
    [TestClass]
    public class FieldBindingTest
    {
#pragma warning disable 649

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

        private class MapAsSampleClass
        {
            [MapAs("SuperField")]
            
            public int Field;
        }
#pragma warning restore 169

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
        [ExpectedException(typeof(ArgumentNullException))]
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
        public void FieldBinding_WithMismatchGenericTypeToPropertyInfoDeclaringType_Throws()
        {
            var fieldName = "PublicInt";
            var sampleObj = new SampleClass();
            var binding = new FieldBinding<AnotherSampleClass>(typeof(SampleClass).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ));
        }

        [TestMethod]
        public void FieldBinding_UsingMapAsAttribute_UsesMapAsName()
        {
            // Arrange
            var fieldName = "Field";
            var mapAsName = "SuperField";

            // Act
            var binding = new FieldBinding<MapAsSampleClass>(typeof(MapAsSampleClass).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

            // Assert
            Assert.AreEqual(mapAsName, binding.Name);
        }
    }
}
