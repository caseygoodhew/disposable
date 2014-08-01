using System;

using Disposable.Data.Packages.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Data.Packages.Test.Core
{
    [TestClass]
    public class InputParameterTest
    {
        [TestMethod]
        public void InputParameter_Constructs_AsExpected()
        {
            var name = "Casey";
            var dataType = DataTypes.Guid;
            var required = new Random().NextDouble() > 0.5;

            var parameter = new InputParameter(name, dataType, required);

            Assert.AreEqual(name, parameter.Name);
            Assert.AreEqual(dataType, parameter.DataType);
            Assert.AreEqual(required, parameter.Required);
        }
    }
}
