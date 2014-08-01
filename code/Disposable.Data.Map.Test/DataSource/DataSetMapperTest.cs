using System;
using System.Data;

using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Binding;
using Disposable.Data.Map.DataSource;
using Disposable.Test.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Map.Test.DataSource
{
    [TestClass]
    public class DataSetMapperTest
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
        public void DataTableMapper_GetOne_CallsUnderlying()
        {
            var dataSet = new DataSet();
            dataSet.Tables.Add();
            
            var mapper = new DataSetMapper();
            mapper.GetOne<SomeClass>(dataSet);
            dataSourceMapperMock.Verify(x => x.GetOne<SomeClass>(It.IsAny<IDataSourceReader>()));
        }

        [TestMethod]
        public void DataTableMapper_GetMany_CallsUnderlying()
        {
            var dataSet = new DataSet();
            dataSet.Tables.Add();

            var mapper = new DataSetMapper();
            mapper.GetMany<SomeClass>(dataSet);
            dataSourceMapperMock.Verify(x => x.GetMany<SomeClass>(It.IsAny<IDataSourceReader>()));
        }
    }
}
