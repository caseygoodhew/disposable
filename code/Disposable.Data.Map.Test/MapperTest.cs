using System;
using System.Data;

using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.DataSource;
using Disposable.Test.Common.ServiceLocator;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Map.Test
{
    [TestClass]
    public class MapperTest
    {
        public class SomeClass {}
        
        private Mock<IDataSourceMapper<DataSet>> dataSetMapperMock;

        private Mock<IDataSourceMapper<IDataReader>> dataReaderMapperMock;
        
        [TestInitialize]
        public void Initialize()
        {
            var locator = Locator.Current as Locator;
            locator.ResetRegsitrars();

            dataSetMapperMock = new Mock<IDataSourceMapper<DataSet>>();
            dataReaderMapperMock = new Mock<IDataSourceMapper<IDataReader>>();

            locator.Register(() => dataSetMapperMock.Object);
            locator.Register(() => dataReaderMapperMock.Object);
        }

        [TestMethod]
        public void GetOne_UsingDataSet_CallsUnderlyingMapper()
        {
            DataSet dataSet = null;
            Mapper.GetOne<SomeClass>(dataSet);
            dataSetMapperMock.Verify(x => x.GetOne<SomeClass>(It.IsAny<DataSet>()), Times.Once);
        }

        [TestMethod]
        public void GetOne_UsingIDataReader_CallsUnderlyingMapper()
        {
            IDataReader dataReader = null;
            Mapper.GetOne<SomeClass>(dataReader);
            dataReaderMapperMock.Verify(x => x.GetOne<SomeClass>(It.IsAny<IDataReader>()), Times.Once);
        }

        [TestMethod]
        public void GetMany_UsingDataSet_CallsUnderlyingMapper()
        {
            DataSet dataSet = null;
            Mapper.GetMany<SomeClass>(dataSet);
            dataSetMapperMock.Verify(x => x.GetMany<SomeClass>(It.IsAny<DataSet>()), Times.Once);
        }

        [TestMethod]
        public void GetMany_UsingIDataReader_CallsUnderlyingMapper()
        {
            IDataReader dataReader = null;
            Mapper.GetMany<SomeClass>(dataReader);
            dataReaderMapperMock.Verify(x => x.GetMany<SomeClass>(It.IsAny<IDataReader>()), Times.Once);
        }
    }
}
