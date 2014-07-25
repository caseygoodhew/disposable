using System;
using System.Data;
using System.Linq.Expressions;

using Disposable.Data.Map.DataSource;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

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
