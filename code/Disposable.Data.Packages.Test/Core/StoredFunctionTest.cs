using System;
using System.Data;
using System.Runtime.InteropServices;

using Disposable.Data.Packages.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Packages.Test.Core
{
    [TestClass]
    public class StoredFunctionTest
    {
        public class StoredFunctionStub : StoredFunction
        {
            public StoredFunctionStub(IPackage package, string name, params IParameter[] parameters)
                : base(package, name, parameters)
            {
            }
        }
        
        [TestMethod]
        public void Construction_WithoutInputParams_Succeeds()
        {
            var package = new Mock<IPackage>();
            var name = "Test Function";
            var outputParameter = new Mock<IOutputParameter>();
            outputParameter.SetupGet(x => x.Direction).Returns(ParameterDirection.ReturnValue);

            var function = new StoredFunctionStub(package.Object, name, outputParameter.Object);

            Assert.AreSame(package.Object, function.Package);
            Assert.AreEqual(name, function.Name);
        }

        [TestMethod]
        public void Construction_WithInputParam_Succeeds()
        {
            var package = new Mock<IPackage>();
            var name = "Test Function";
            var outputParameter = new Mock<IOutputParameter>();
            outputParameter.SetupGet(x => x.Direction).Returns(ParameterDirection.ReturnValue);
            var inputParam = new Mock<IInputParameter>();

            var function = new StoredFunctionStub(package.Object, name, outputParameter.Object, inputParam.Object);

            Assert.AreSame(package.Object, function.Package);
            Assert.AreEqual(name, function.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construction_WithNoOutputParams_Throws()
        {
            new StoredFunctionStub(new Mock<IPackage>().Object, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construction_WithTwoOutputParams_Throws()
        {
            var outputParameter1 = new Mock<IOutputParameter>();
            var outputParameter2 = new Mock<IOutputParameter>();

            new StoredFunctionStub(new Mock<IPackage>().Object, string.Empty, outputParameter1.Object, outputParameter2.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construction_WithOnwOutputParamAndInputDirection_Throws()
        {
            var outputParameter = new Mock<IOutputParameter>();
            outputParameter.SetupGet(x => x.Direction).Returns(ParameterDirection.Input);

            new StoredFunctionStub(new Mock<IPackage>().Object, string.Empty, outputParameter.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construction_WithOnwOutputParamAndInputOutputDirection_Throws()
        {
            var outputParameter = new Mock<IOutputParameter>();
            outputParameter.SetupGet(x => x.Direction).Returns(ParameterDirection.InputOutput);

            new StoredFunctionStub(new Mock<IPackage>().Object, string.Empty, outputParameter.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construction_WithOnwOutputParamAndOutputDirection_Throws()
        {
            var outputParameter = new Mock<IOutputParameter>();
            outputParameter.SetupGet(x => x.Direction).Returns(ParameterDirection.Output);

            new StoredFunctionStub(new Mock<IPackage>().Object, string.Empty, outputParameter.Object);
        }
    }
}

