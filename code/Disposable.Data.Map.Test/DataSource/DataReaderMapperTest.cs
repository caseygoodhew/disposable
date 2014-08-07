using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.DataSource;
using Disposable.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;

namespace Disposable.Data.Map.Test.DataSource
{
    [TestClass]
    public class DataReaderMapperTest
    {
        private class SomeClass { }

        private Mock<IDataSourceMapper<IDataSourceReader>> dataSourceMapperMock;
        
        [TestInitialize]
        public void Initialize()
        {
            var locator = Locator.Current as Locator;
            locator.ResetRegsitrars();

            dataSourceMapperMock = new Mock<IDataSourceMapper<IDataSourceReader>>();

            // ReSharper disable once PossibleNullReferenceException
            locator.Register(() => dataSourceMapperMock.Object);
        }
        
        [TestMethod]
        public void DataReaderMapper_GetOne_CallsUnderlying()
        {
            var dataReaderMock = new Mock<IDataReader>();

            var mapper = new DataReaderMapper();
            mapper.GetOne<SomeClass>(dataReaderMock.Object);
            dataSourceMapperMock.Verify(x => x.GetOne<SomeClass>(It.IsAny<IDataSourceReader>()));
        }

        [TestMethod]
        public void DataReaderMapper_GetMany_CallsUnderlying()
        {
            var dataReaderMock = new Mock<IDataReader>();

            var mapper = new DataReaderMapper();
            mapper.GetMany<SomeClass>(dataReaderMock.Object);
            dataSourceMapperMock.Verify(x => x.GetMany<SomeClass>(It.IsAny<IDataSourceReader>()));
        }
    }
}
