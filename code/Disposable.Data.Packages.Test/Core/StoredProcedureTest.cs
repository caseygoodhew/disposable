using System;
using System.Data;
using System.Runtime.InteropServices;

using Disposable.Data.Packages.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Packages.Test.Core
{
    [TestClass]
    public class StoredProcedureTest
    {
        public class StoredProcedureStub : StoredProcedure
        {
            public StoredProcedureStub(IPackage package, string name, params IParameter[] parameters)
                : base(package, name, parameters)
            {
            }
        }

        [TestMethod]
        public void Construction_WithNoParams_Succeeds()
        {
            var package = new Mock<IPackage>();
            var name = "Test Function";
            
            var procedure = new StoredProcedureStub(package.Object, name);

            Assert.AreSame(package.Object, procedure.Package);
            Assert.AreEqual(name, procedure.Name);
        }

        [TestMethod]
        public void Construction_WithInputParam_Succeeds()
        {
            var package = new Mock<IPackage>();
            var name = "Test Function";
            var inputParam = new Mock<IInputParameter>();

            var function = new StoredProcedureStub(package.Object, name, inputParam.Object);

            Assert.AreSame(package.Object, function.Package);
            Assert.AreEqual(name, function.Name);
        }

        [TestMethod]
        public void Construction_WithInputParams_Succeeds()
        {
            var package = new Mock<IPackage>();
            var name = "Test Function";
            var inputParam1 = new Mock<IInputParameter>();
            var inputParam2 = new Mock<IInputParameter>();

            var function = new StoredProcedureStub(package.Object, name, inputParam1.Object, inputParam2.Object);

            Assert.AreSame(package.Object, function.Package);
            Assert.AreEqual(name, function.Name);
        }

        [TestMethod]
        public void Construction_WithOutputParam_Succeeds()
        {
            var package = new Mock<IPackage>();
            var name = "Test Function";
            var outputParameter = new Mock<IOutputParameter>();
            outputParameter.SetupGet(x => x.Direction).Returns(ParameterDirection.Output);

            var function = new StoredProcedureStub(package.Object, name, outputParameter.Object, outputParameter.Object);

            Assert.AreSame(package.Object, function.Package);
            Assert.AreEqual(name, function.Name);
        }

        [TestMethod]
        public void Construction_WithOutputParams_Succeeds()
        {
            var package = new Mock<IPackage>();
            var name = "Test Function";
            var outputParameter1 = new Mock<IOutputParameter>();
            outputParameter1.SetupGet(x => x.Direction).Returns(ParameterDirection.Output);
            var outputParameter2 = new Mock<IOutputParameter>();
            outputParameter2.SetupGet(x => x.Direction).Returns(ParameterDirection.Output);

            var function = new StoredProcedureStub(package.Object, name, outputParameter1.Object, outputParameter2.Object);

            Assert.AreSame(package.Object, function.Package);
            Assert.AreEqual(name, function.Name);
        }

        [TestMethod]
        public void Construction_WithMixedParams_Succeeds()
        {
            var package = new Mock<IPackage>();
            var name = "Test Function";
            var inputParam1 = new Mock<IInputParameter>();
            var inputParam2 = new Mock<IInputParameter>();
            var outputParameter1 = new Mock<IOutputParameter>();
            outputParameter1.SetupGet(x => x.Direction).Returns(ParameterDirection.Output);
            var outputParameter2 = new Mock<IOutputParameter>();
            outputParameter2.SetupGet(x => x.Direction).Returns(ParameterDirection.Output);

            var function = new StoredProcedureStub(package.Object, name, inputParam1.Object, inputParam2.Object, outputParameter1.Object, outputParameter2.Object);

            Assert.AreSame(package.Object, function.Package);
            Assert.AreEqual(name, function.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construction_WithOutputParamInputDirection_Throws()
        {
            var outputParameter = new Mock<IOutputParameter>();
            outputParameter.SetupGet(x => x.Direction).Returns(ParameterDirection.Input);

            new StoredProcedureStub(new Mock<IPackage>().Object, string.Empty, outputParameter.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construction_WithOutputParamInputOutputDirection_Throws()
        {
            var outputParameter = new Mock<IOutputParameter>();
            outputParameter.SetupGet(x => x.Direction).Returns(ParameterDirection.InputOutput);

            new StoredProcedureStub(new Mock<IPackage>().Object, string.Empty, outputParameter.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construction_WithOutputParamReturnValueDirection_Throws()
        {
            var outputParameter = new Mock<IOutputParameter>();
            outputParameter.SetupGet(x => x.Direction).Returns(ParameterDirection.ReturnValue);

            new StoredProcedureStub(new Mock<IPackage>().Object, string.Empty, outputParameter.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construction_WithTwoOutputParamOneWithInputDirection_Throws()
        {
            var outputParameter1 = new Mock<IOutputParameter>();
            outputParameter1.SetupGet(x => x.Direction).Returns(ParameterDirection.Input);
            var outputParameter2 = new Mock<IOutputParameter>();
            outputParameter2.SetupGet(x => x.Direction).Returns(ParameterDirection.Output);

            new StoredProcedureStub(new Mock<IPackage>().Object, string.Empty, outputParameter1.Object, outputParameter2.Object);
        }
    }
}

