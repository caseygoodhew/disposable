using Disposable.Data.Map.DataSource;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data;
using System.Linq.Expressions;

namespace Disposable.Data.Map.Test.DataSource
{
    [TestClass]
    public class DataReaderAdapterTest
    {
        [TestMethod]
        public void DataReaderAdapter_CallingOverrideMethods_CallsToUnderlying()
        {
            var mock = new Mock<IDataReader>();

            var adapter = new DataReaderAdapter(mock.Object);
            
            AssertCallsOnce(mock, adapter, x => x.GetBoolean(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetByte(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetChar(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetDataTypeName(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetDateTime(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetDecimal(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetDouble(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetFieldType(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetFloat(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetInt16(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetInt32(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetInt64(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetName(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetSchemaTable());
            AssertCallsOnce(mock, adapter, x => x.GetString(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetValue(It.IsAny<int>()));
            AssertCallsOnce(mock, adapter, x => x.GetValues(It.IsAny<object[]>()));
            AssertCallsOnce(mock, adapter, x => x.IsDBNull(It.IsAny<int>()));

            var fieldCount = adapter.FieldCount;
            mock.VerifyGet(x => x.FieldCount, Times.Once());
            
            adapter.InternalRead();
            mock.Verify(x => x.Read(), Times.Once);
        }
        
        private static void AssertCallsOnce(Mock<IDataReader> mock, DataReaderAdapter adapter, Expression<Action<IDataReader>> expression)
        {
            expression.Compile().Invoke(adapter);
            mock.Verify(expression, Times.Once);
        }
  }
}
