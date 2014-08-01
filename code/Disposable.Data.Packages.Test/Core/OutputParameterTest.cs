using System;

using Disposable.Data.Packages.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Data.Packages.Test.Core
{
    [TestClass]
    public class OutputParameterTest
    {
        [TestMethod]
        public void OutputParameter_Constructs_AsExpected()
        {
            var name = "Casey";
            var dataType = DataTypes.Guid;
            
            var parameter = new OutputParameter(name, dataType);

            Assert.AreEqual(name, parameter.Name);
            Assert.AreEqual(dataType, parameter.DataType);
        }
    }
}
