using System;
using System.Collections.Generic;
using System.Linq;

using Disposable.Data.Common.Exceptions;
using Disposable.Data.Packages.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Data.Packages.Test.Core
{
    [TestClass]
    public class StoredMethodTest
    {
        public class StoredMethodStub : StoredMethod
        {
            public StoredMethodStub(params IParameter[] parameters)
                : base(new Mock<IPackage>().Object, string.Empty, parameters)
            {
            }

            public StoredMethodStub(IPackage package, string name, params IParameter[] parameters)
                : base(package, name, parameters)
            {
            }

            public new IStoredMethodInstance CreateInstance()
            {
                return base.CreateInstance();
            }

            public IList<IInputParameter> GetInputParameters()
            {
                return InputParameters;
            }

            public IList<IOutputParameter> GetOutputParameters()
            {
                return OutputParameters;
            }
        }
        
        [TestMethod]
        public void StoredMethod_WithNoParameters_ConstructsAsExpected()
        {
            var package = new Mock<IPackage>();
            var name = "Casey";

            var storedMethod = new StoredMethodStub(package.Object, name);

            Assert.AreSame(package.Object, storedMethod.Package);
            Assert.AreEqual(name, storedMethod.Name);
        }

        [TestMethod]
        public void StoredMethod_WithInputParameters_ConstructsAsExpected()
        {
            var inParam1 = new Mock<IInputParameter>();
            var inParam2 = new Mock<IInputParameter>();
            var inParam3 = new Mock<IInputParameter>();

            var storedMethod = new StoredMethodStub(inParam1.Object, inParam2.Object, inParam3.Object);
            var inputs = storedMethod.GetInputParameters();
            var outputs = storedMethod.GetOutputParameters();

            Assert.AreEqual(3, inputs.Count);
            Assert.AreSame(inParam1.Object, inputs.First());
            Assert.AreSame(inParam2.Object, inputs.Skip(1).First());
            Assert.AreSame(inParam3.Object, inputs.Skip(2).First());

            Assert.AreEqual(0, outputs.Count);
        }

        [TestMethod]
        public void StoredMethod_WithOutputParameters_ConstructsAsExpected()
        {
            var outParam1 = new Mock<IOutputParameter>();
            var outParam2 = new Mock<IOutputParameter>();
            var outParam3 = new Mock<IOutputParameter>();

            var storedMethod = new StoredMethodStub(outParam1.Object, outParam2.Object, outParam3.Object);
            var inputs = storedMethod.GetInputParameters();
            var outputs = storedMethod.GetOutputParameters();

            Assert.AreEqual(0, inputs.Count);

            Assert.AreEqual(3, outputs.Count);
            Assert.AreSame(outParam1.Object, outputs.First());
            Assert.AreSame(outParam2.Object, outputs.Skip(1).First());
            Assert.AreSame(outParam3.Object, outputs.Skip(2).First());
        }

        [TestMethod]
        public void StoredMethod_WithMixedParameters_ConstructsAsExpected()
        {
            var inParam1 = new Mock<IInputParameter>();
            var inParam2 = new Mock<IInputParameter>();
            var inParam3 = new Mock<IInputParameter>();

            var outParam1 = new Mock<IOutputParameter>();
            var outParam2 = new Mock<IOutputParameter>();
            var outParam3 = new Mock<IOutputParameter>();

            var storedMethod = new StoredMethodStub(inParam1.Object, outParam1.Object, inParam2.Object, outParam2.Object, inParam3.Object, outParam3.Object);
            var inputs = storedMethod.GetInputParameters();
            var outputs = storedMethod.GetOutputParameters();

            Assert.AreEqual(3, inputs.Count);
            Assert.AreSame(inParam1.Object, inputs.First());
            Assert.AreSame(inParam2.Object, inputs.Skip(1).First());
            Assert.AreSame(inParam3.Object, inputs.Skip(2).First());

            Assert.AreEqual(3, outputs.Count);
            Assert.AreSame(outParam1.Object, outputs.First());
            Assert.AreSame(outParam2.Object, outputs.Skip(1).First());
            Assert.AreSame(outParam3.Object, outputs.Skip(2).First());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StoredMethod_WithInvalidParameter_Throws()
        {
            var inParam = new Mock<IInputParameter>();
            var outParam = new Mock<IOutputParameter>();
            var param = new Mock<IParameter>();

            new StoredMethodStub(inParam.Object, outParam.Object, param.Object);
        }

        [TestMethod]
        public void Handle_WithNullParameters_ReturnsUnhandled()
        {
            var storedMethod = new StoredMethodStub();
            Assert.AreEqual(ProgrammaticDatabaseExceptions.Unhandled, storedMethod.Handle(null, null, null));
        }

        [TestMethod]
        public void CreateInstance_WithNoParameters_ReturnsIStoredMethodInstance()
        {
            var storedMethod = new StoredMethodStub();
            var instance = storedMethod.CreateInstance();
            Assert.IsInstanceOfType(instance, typeof(IStoredMethodInstance));

            Assert.AreEqual(0, instance.GetValues<IInputParameterValue>(true).Count);
            Assert.AreEqual(0, instance.GetValues<IOutputParameterValue>(true).Count);
        }
        
        [TestMethod]
        public void CreateInstance_WithParameters_ReturnsIStoredMethodInstance()
        {
            var inParam = new Mock<IInputParameter>();
            var outParam = new Mock<IOutputParameter>();
            var storedMethod = new StoredMethodStub(inParam.Object, outParam.Object);
            var instance = storedMethod.CreateInstance();
            Assert.IsInstanceOfType(instance, typeof(IStoredMethodInstance));

            Assert.AreEqual(1, instance.GetValues<IInputParameterValue>(true).Count);
            Assert.AreEqual(1, instance.GetValues<IOutputParameterValue>(true).Count);

            Assert.AreSame(inParam.Object, instance.GetValues<IInputParameterValue>(true).First().AsInputParameter());
            Assert.AreSame(outParam.Object, instance.GetValues<IOutputParameterValue>(true).First().AsOutputParameter());
        }
    }
}
