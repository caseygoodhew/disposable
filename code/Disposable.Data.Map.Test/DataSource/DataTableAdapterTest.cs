using Disposable.Data.Map.DataSource;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;

namespace Disposable.Data.Map.Test.DataSource
{
    [TestClass]
    public class DataTableAdapterTest
    {
        [TestMethod]
        public void DataTableAdapter_CallingOverrideMethods_CallsToUnderlying()
        {
            var dataTableMock = new Mock<DataTable>();
            new DataTableAdapter(dataTableMock.Object);
        }
  }
}
