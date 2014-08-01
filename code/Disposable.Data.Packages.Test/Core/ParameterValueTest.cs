using System;

using Disposable.Data.Packages.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Packages.Test.Core
{
    [TestClass]
    public class ParameterValueTest
    {
        [TestMethod]
        public void ParameterValue_Constructs_AsExpected()
        {
            var name = "Casey";
            var dataType = DataTypes.Long;
            var value = 42;

            var parameterMock = new Mock<Parameter>(name, dataType);
            var parameterValueMock = new Mock<ParameterValue<IParameter>>(parameterMock.Object, value);
            
            Assert.AreEqual(name, parameterValueMock.Object.Name);
            Assert.AreEqual(dataType, parameterValueMock.Object.DataType);
            Assert.AreEqual(value, parameterValueMock.Object.Value);
        }
    }
}
