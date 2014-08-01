using System;

using Disposable.Data.Packages.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Data.Packages.Test.Core
{
    [TestClass]
    public class InputParameterValueTest
    {
        [TestMethod]
        public void InputParameterValue_Constructs_AsExpected()
        {
            var name = "Casey";
            var dataType = DataTypes.Guid;
            var required = new Random().NextDouble() > 0.5;
            var value = 42;

            var parameter = new InputParameter(name, dataType, required);
            var parameterValue = new InputParameterValue(parameter, value);

            Assert.AreEqual(name, parameterValue.Name);
            Assert.AreEqual(dataType, parameterValue.DataType);
            Assert.AreEqual(required, parameterValue.Required);
            Assert.AreEqual(value, parameterValue.Value);
        }
    }
}
