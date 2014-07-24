using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

using Disposable.Data.Map.DataSource;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Map.Test.DataSource
{
    [TestClass]
    public class DataReaderAdapterTest
    {
        private int depth;

        [TestMethod]
        public void DataReaderAdapter_CallingAllMethodsAndProperties_RunsAsExpected()
        {
            var mock = new Mock<IDataReader>();
            var fields = new[] { "TestOne", "TestTwo" };
            
            var adapter = new DataReaderAdapter(mock.Object);

            AssertThrows<NotImplementedException>(adapter, x => x.Close());
            AssertThrows<NotSupportedException>(adapter, x => x.Dispose());
            AssertCallsOnce(mock, adapter, x => x.GetBoolean(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetByte(It.IsAny<int>()));
            AssertThrows<NotImplementedException>(adapter, x => x.GetBytes(It.IsAny<int>(), It.IsAny<long>(), It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetChar(It.IsAny<int>()));
            AssertThrows<NotImplementedException>(adapter, x => x.GetChars(It.IsAny<int>(), It.IsAny<long>(), It.IsAny<char[]>(), It.IsAny<int>(), It.IsAny<int>()));
            AssertThrows<NotImplementedException>(adapter, x => x.GetData(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetDataTypeName(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetDateTime(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetDecimal(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetDouble(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetFieldType(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetFloat(It.IsAny<int>()));
            AssertThrows<NotImplementedException>(adapter, x => x.GetGuid(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetInt16(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetInt32(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetInt64(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetName(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetSchemaTable());
            AssertCallsOnce(mock, adapter, x => x.GetString(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetValue(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetValues(It.IsAny<object[]>()));
            AssertCallsOnce(mock, adapter, x => x.IsDBNull(It.IsAny<int>()));
            AssertThrows<NotImplementedException>(adapter, x => x.NextResult());
            AssertThrows<InvalidOperationException>(adapter, x => x.Read());

            mock.SetupGet(x => x.FieldCount).Returns(fields.Count());
            var fieldCount = adapter.FieldCount;
            mock.VerifyGet(x => x.FieldCount, Times.Once);

            AssertThrows<NotImplementedException>(adapter, x => x.Depth);
            AssertThrows<NotImplementedException>(adapter, x => x.IsClosed);
            AssertThrows<NotImplementedException>(adapter, x => x.RecordsAffected);

            mock.Setup(x => x.GetName(It.IsAny<int>())).Returns<int>((index) => fields[index]);

            Assert.AreEqual(0, adapter.GetOrdinal(fields.First()));
            Assert.AreEqual(0, adapter.GetOrdinal("test_one"));
            Assert.AreEqual(1, adapter.GetOrdinal(fields.Skip(1).First()));
            AssertThrows<IndexOutOfRangeException>(adapter, x => x.GetOrdinal(fields.First() + fields.First()));
            Assert.IsTrue(adapter.HasOrdinal(fields.First()));
            Assert.IsFalse(adapter.HasOrdinal(fields.First() + fields.First()));
            
            adapter.InternalRead();
            mock.Verify(x => x.Read(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void DataReaderAdapter_IndexByInt_Throws()
        {
            var adapter = new DataReaderAdapter(new Mock<IDataReader>().Object);
            var test = adapter[0];
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void DataReaderAdapter_IndexByString_Throws()
        {
            var adapter = new DataReaderAdapter(new Mock<IDataReader>().Object);
            var test = adapter["test"];
        }
        
        private static void AssertThrows<TException>(DataReaderAdapter adapter, Expression<Func<IDataReader, object>> expression) where TException : SystemException
        {
            var threw = true;

            try
            {
                expression.Compile().Invoke(adapter);
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
        
        private static void AssertThrows<TException>(DataReaderAdapter adapter, Expression<Action<IDataReader>> expression) where TException : SystemException
        {
            var threw = true;
            
            try
            {
                expression.Compile().Invoke(adapter);
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

        private static void AssertCallsOnce(Mock<IDataReader> mock, DataReaderAdapter adapter, Expression<Action<IDataReader>> expression)
        {
            expression.Compile().Invoke(adapter);
            mock.Verify(expression, Times.Once);
        }
    }
}
