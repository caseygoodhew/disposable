using System;
using System.Linq;

using Disposable.Data.Map.DataSource;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Map.Test.DataSource
{
    [TestClass]
    public class DataSourceReaderTest
    {
        [TestMethod]
        public void DataSourceReaderTest_GetOrdingalHasOrdinal_ReturnsExpected()
        {
            var fields = new[] { "TestOne", "TestTwo" };

            var dataSourceReaderMock = new Mock<DataSourceReader>();
            var dataSourceReader = dataSourceReaderMock.Object;

            dataSourceReaderMock.SetupGet(x => x.FieldCount).Returns(fields.Count());
            dataSourceReaderMock.Setup(x => x.GetName(It.IsAny<int>())).Returns<int>((index) => fields[index]);
            
            // Assert GetOrdinal
            Assert.AreEqual(0, dataSourceReader.GetOrdinal(fields.First()));
            Assert.AreEqual(0, dataSourceReader.GetOrdinal("test_one"));
            Assert.AreEqual(1, dataSourceReader.GetOrdinal(fields.Skip(1).First()));
            AssertThrows<IndexOutOfRangeException>(dataSourceReader, x => x.GetOrdinal(fields.First() + fields.First()));

            // Assert HasOrdinal
            Assert.IsTrue(dataSourceReader.HasOrdinal(fields.First()));
            Assert.IsFalse(dataSourceReader.HasOrdinal(fields.First() + fields.First()));
        }
        
        [TestMethod]
        public void DataSourceReaderTest_CallingMethodsAndPropertiesThatShouldThrow_Throws()
        {
            var dataSourceReader = new Mock<DataSourceReader>();

            AssertThrows<NotImplementedException>(dataSourceReader.Object, x => x.Close());
            AssertThrows<NotImplementedException>(dataSourceReader.Object, x => x.Depth);
            AssertThrows<NotSupportedException>(dataSourceReader.Object, x => x.Dispose());
            AssertThrows<NotImplementedException>(dataSourceReader.Object, x => x.GetBytes(It.IsAny<int>(), It.IsAny<long>(), It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()));
            AssertThrows<NotImplementedException>(dataSourceReader.Object, x => x.GetChars(It.IsAny<int>(), It.IsAny<long>(), It.IsAny<char[]>(), It.IsAny<int>(), It.IsAny<int>()));
            AssertThrows<NotImplementedException>(dataSourceReader.Object, x => x.GetData(It.IsAny<int>()));
            AssertThrows<NotImplementedException>(dataSourceReader.Object, x => x.GetGuid(It.IsAny<int>()));
            AssertThrows<NotImplementedException>(dataSourceReader.Object, x => x.IsClosed);
            AssertThrows<NotImplementedException>(dataSourceReader.Object, x => x.NextResult());
            AssertThrows<InvalidOperationException>(dataSourceReader.Object, x => x.Read());
            AssertThrows<NotImplementedException>(dataSourceReader.Object, x => x.RecordsAffected);
        }

        [TestMethod]
        public void DataReaderAdapter_IndexByInt_CallsToValue()
        {
            var dataSourceReader = new Mock<DataSourceReader>();
            var test = dataSourceReader.Object[0];
            dataSourceReader.Verify(x => x.GetValue(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void DataReaderAdapter_IndexByString_Throws()
        {
            var fields = new[] { "TestOne", "TestTwo" };
            
            var dataSourceReader = new Mock<DataSourceReader>();
            dataSourceReader.SetupGet(x => x.FieldCount).Returns(fields.Count());
            dataSourceReader.Setup(x => x.GetName(It.IsAny<int>())).Returns<int>((index) => fields[index]);

            var test = dataSourceReader.Object[fields.First()];
            dataSourceReader.Verify(x => x.GetValue(It.IsAny<int>()), Times.Once);
        }
        
        private static void AssertThrows<TException>(DataSourceReader dataSourceReader, Action<DataSourceReader> action) where TException : SystemException
        {
            AssertThrows<TException>(dataSourceReader, x => { action(x); return true; });
        }

        private static void AssertThrows<TException>(DataSourceReader dataSourceReader, Func<DataSourceReader, object> func) where TException : SystemException
        {
            var threw = true;

            try
            {
                func.Invoke(dataSourceReader);
                threw = false;
            }
            catch (SystemException e)
            {
                if (e.GetType() != typeof(TException))
                {
                    throw;
                }
            }

            if (!threw)
            {
                throw new InvalidOperationException("Target did not throw exception.");
            }
        }
    }
}
