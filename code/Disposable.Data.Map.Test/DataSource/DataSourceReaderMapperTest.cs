using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;

using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Binding;
using Disposable.Data.Map.DataSource;
using Disposable.Test.Common.ServiceLocator;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Map.Test.DataSource
{
    [TestClass]
    public class DataSourceReaderMapperTest
    {
        private class InvalidTestOperationException : InvalidOperationException { }

        public class SomeClass
        {
            public int FieldOne;
            
            public string FieldTwo;
            
            public bool PropertyOne { get; set; }

            public long PropertyTwo { get; set; }
        }
        
        private Mock<ITypeBindingFactory> typeBindingFactoryMock;
        
        // can't do this as IDataSourceReader mock as we can't mock out params in TryGetOrdinal
        private Mock<IDataReader> dataReaderMock;

        private int dataIndex = -1;
        
        private List<SomeClass> data;

        private List<Mock<IMemberBinding<SomeClass>>> memberBindingMocks;

        private Mock<ITypeBinding<SomeClass>> typeBindingMock;

        [TestInitialize]
        public void Initialize()
        {
            var locator = Locator.Current as Locator;
            locator.ResetRegsitrars();

            memberBindingMocks = new List<Mock<IMemberBinding<SomeClass>>>()
                                         {
                                             new Mock<IMemberBinding<SomeClass>>(MockBehavior.Strict), 
                                             new Mock<IMemberBinding<SomeClass>>(MockBehavior.Strict),
                                             new Mock<IMemberBinding<SomeClass>>(MockBehavior.Strict),
                                             new Mock<IMemberBinding<SomeClass>>(MockBehavior.Strict)
                                         };

            memberBindingMocks[0].SetupGet(x => x.Name).Returns("FieldOne");
            memberBindingMocks[0].SetupGet(x => x.DataType).Returns(typeof(int));

            memberBindingMocks[1].SetupGet(x => x.Name).Returns("FieldTwo");
            memberBindingMocks[1].SetupGet(x => x.DataType).Returns(typeof(string));

            memberBindingMocks[2].SetupGet(x => x.Name).Returns("PropertyOne");
            memberBindingMocks[2].SetupGet(x => x.DataType).Returns(typeof(bool));

            memberBindingMocks[3].SetupGet(x => x.Name).Returns("PropertyTwo");
            memberBindingMocks[3].SetupGet(x => x.DataType).Returns(typeof(long));
            
            memberBindingMocks.ForEach(m => m.Setup(x => x.SetValue(It.IsAny<SomeClass>(), It.IsAny<object>())).Callback<SomeClass, object>(
                (obj, value) =>
                    {
                        if (value is int)
                        {
                            obj.FieldOne = (int)value;
                        }
                        else if (value is string)
                        {
                            obj.FieldTwo = (string)value;
                        }
                        else if (value is bool)
                        {
                            obj.PropertyOne = (bool)value;
                        }
                        else if (value is long)
                        {
                            obj.PropertyTwo = (long)value;
                        }
                    }));
            
            typeBindingMock = new Mock<ITypeBinding<SomeClass>>(MockBehavior.Strict);
            typeBindingMock.Setup(x => x.BeginMapping(It.IsAny<SomeClass>(), It.IsAny<IDataSourceReader>()));
            typeBindingMock.Setup(x => x.EndMapping(It.IsAny<SomeClass>(), It.IsAny<IDataSourceReader>()));
            typeBindingMock.Setup(x => x.GetEnumerator()).Returns(() => memberBindingMocks.Select(x => x.Object).GetEnumerator());
            
            typeBindingFactoryMock = new Mock<ITypeBindingFactory>(MockBehavior.Strict);
            typeBindingFactoryMock.Setup(x => x.Get<SomeClass>(It.IsAny<IDataSourceReader>())).Returns(() => typeBindingMock.Object);

            // ReSharper disable once PossibleNullReferenceException
            locator.Register(() => typeBindingFactoryMock.Object);

            data = new List<SomeClass>();
            
            dataReaderMock = new Mock<IDataReader>(MockBehavior.Strict);
            dataReaderMock.Setup(x => x.Read()).Returns(
                () =>
                    {
                        dataIndex++;
                        return dataIndex < data.Count;
                    });
            dataReaderMock.SetupGet(x => x.FieldCount).Returns(() => memberBindingMocks.Count);
            dataReaderMock.Setup(x => x.GetName(It.IsAny<int>())).Returns<int>((index) => memberBindingMocks[index].Object.Name);
            dataReaderMock.Setup(x => x.GetValue(It.IsAny<int>())).Returns<int>(
                (index) =>
                    {
                        var record = data[dataIndex];
                        if (index == 0)
                        {
                            return record.FieldOne;
                        }

                        if (index == 1)
                        {
                            return record.FieldTwo;
                        }

                        if (index == 2)
                        {
                            return record.PropertyOne;
                        }

                        if (index == 3)
                        {
                            return record.PropertyTwo;
                        }

                        return null;
                    });
        }

        [TestMethod]
        public void GetOne_WithOneRecord_Succeeds()
        {
            var record = AddRecord();

            var mapper = new DataSourceReaderMapper();
            var result = mapper.GetOne<SomeClass>(new DataReaderAdapter(dataReaderMock.Object));

            AssertAreEqual(record, result);
        }

        [TestMethod]
        [ExpectedException(typeof(MapperException))]
        public void GetOne_WithNoRecords_Throws()
        {
            var mapper = new DataSourceReaderMapper();
            mapper.GetOne<SomeClass>(new DataReaderAdapter(dataReaderMock.Object));
        }

        [TestMethod]
        [ExpectedException(typeof(MapperException))]
        public void GetOne_WithTwoRecords_Throws()
        {
            AddRecord();
            AddRecord();
            var mapper = new DataSourceReaderMapper();
            mapper.GetOne<SomeClass>(new DataReaderAdapter(dataReaderMock.Object));
        }

        [TestMethod]
        public void GetMany_WithNoRecords_Succeeds()
        {
            var mapper = new DataSourceReaderMapper();
            var result = mapper.GetMany<SomeClass>(new DataReaderAdapter(dataReaderMock.Object)).ToList();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetMany_WithOneRecord_Succeeds()
        {
            var record = AddRecord();

            var mapper = new DataSourceReaderMapper();
            var result = mapper.GetMany<SomeClass>(new DataReaderAdapter(dataReaderMock.Object)).ToList();

            Assert.AreEqual(1, result.Count);
            AssertAreEqual(record, result.First());
        }

        [TestMethod]
        public void GetMany_WithTwoRecords_Succeeds()
        {
            var recordOne = AddRecord(1, "casey", true, 478934286);
            var recordTwo = AddRecord(-15, "goodhew", false, 3798324987);

            var mapper = new DataSourceReaderMapper();
            var result = mapper.GetMany<SomeClass>(new DataReaderAdapter(dataReaderMock.Object)).ToList();

            Assert.AreEqual(2, result.Count);
            AssertAreEqual(recordOne, result.First());
            AssertAreEqual(recordTwo, result.Skip(1).First());
        }

        [TestMethod]
        public void GetMany_WithManyRecords_Succeeds()
        {
            var records = Enumerable.Range(1, 100).Select(x => AddRecord(x)).ToList();
            
            var mapper = new DataSourceReaderMapper();
            var result = mapper.GetMany<SomeClass>(new DataReaderAdapter(dataReaderMock.Object)).ToList();

            Assert.AreEqual(records.Count, result.Count);

            for (var i = 0; i < records.Count; i++)
            {
                AssertAreEqual(records[i], result[i]);
            }
        }

        private static void AssertAreEqual(SomeClass expected, SomeClass actual)
        {
            Assert.AreEqual(expected.FieldOne, actual.FieldOne);
            Assert.AreEqual(expected.FieldTwo, actual.FieldTwo);
            Assert.AreEqual(expected.PropertyOne, actual.PropertyOne);
            Assert.AreEqual(expected.PropertyTwo, actual.PropertyTwo);
        }

        private SomeClass AddRecord(int intVal = 42, string strVal = "Test", bool boolVal = true, long longVal = 123456789)
        {
            if (dataIndex >= 0)
            {
                throw new InvalidTestOperationException();
            }

            var record = new SomeClass
                             {
                                 FieldOne = intVal,
                                 FieldTwo = strVal,
                                 PropertyOne = boolVal,
                                 PropertyTwo = longVal
                             };
            
            data.Add(record);

            return record;
        }
    }
}
