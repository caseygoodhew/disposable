using System;
using System.ComponentModel;
using System.Data;

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
            var parameterDirection = ParameterDirection.Output;

            var parameter = new OutputParameter(name, dataType, parameterDirection);

            Assert.AreEqual(name, parameter.Name);
            Assert.AreEqual(dataType, parameter.DataType);
            Assert.AreEqual(parameterDirection, parameter.Direction);
        }

        [TestMethod]
        public void OutputParameter_DefaultDirection_IsOutput()
        {
            var parameter = new OutputParameter(string.Empty, DataTypes.Boolean);

            Assert.AreEqual(ParameterDirection.Output, parameter.Direction);
        }

        [TestMethod]
        public void OutputParameter_WithParameterDirectionOutput_Succeeds()
        {
            var parameterDirection = ParameterDirection.Output;

            var parameter = new OutputParameter(string.Empty, DataTypes.Boolean, parameterDirection);

            Assert.AreEqual(parameterDirection, parameter.Direction);
        }

        [TestMethod]
        public void OutputParameter_WithParameterDirectionReturnValue_Succeeds()
        {
            var parameterDirection = ParameterDirection.ReturnValue;

            var parameter = new OutputParameter(string.Empty, DataTypes.Boolean, parameterDirection);

            Assert.AreEqual(parameterDirection, parameter.Direction);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEnumArgumentException))]
        public void OutputParameter_WithParameterDirectionReturnValue_Throws()
        {
            var parameterDirection = ParameterDirection.Input;

            new OutputParameter(string.Empty, DataTypes.Boolean, parameterDirection);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEnumArgumentException))]
        public void OutputParameter_WithParameterDirectionInputOutput_Throws()
        {
            var parameterDirection = ParameterDirection.InputOutput;

            new OutputParameter(string.Empty, DataTypes.Boolean, parameterDirection);
        }
    }
}
