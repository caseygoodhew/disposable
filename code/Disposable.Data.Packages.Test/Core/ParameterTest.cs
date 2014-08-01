using System;

using Disposable.Data.Packages.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Packages.Test.Core
{
    [TestClass]
    public class ParameterTest
    {
        [TestMethod]
        public void Parameter_Constructs_AsExpected()
        {
            var name = "Casey";
            var dataType = DataTypes.Guid;

            var parameterMock = new Mock<Parameter>(name, dataType);
            
            Assert.AreEqual(name, parameterMock.Object.Name);
            Assert.AreEqual(dataType, parameterMock.Object.DataType);
        }
    }
}
