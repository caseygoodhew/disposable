using System;

using Disposable.Data.Packages.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Data.Packages.Test.Core
{
    [TestClass]
    public class OutputParameterValueTest
    {
        [TestMethod]
        public void OutputParameterValue_Constructs_AsExpected()
        {
            var name = "Casey";
            var dataType = DataTypes.Guid;
            var value = 42;

            var parameter = new OutputParameter(name, dataType);
            var parameterValue = new OutputParameterValue(parameter, value);

            Assert.AreEqual(name, parameterValue.Name);
            Assert.AreEqual(dataType, parameterValue.DataType);
            Assert.AreEqual(value, parameterValue.Value);
            Assert.AreSame(parameter, parameterValue.AsOutputParameter());
        }
    }
}
