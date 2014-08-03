using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;

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
            public StoredMethodStub(params IInputParameter[] inputParameters)
                : base(new Mock<IPackage>().Object, string.Empty, inputParameters)
            {
            }

            public StoredMethodStub(IPackage package, string name, params IInputParameter[] inputParameters)
                : base(package, name, inputParameters)
            {
            }

            public void SetInputParameterValuesTest(IDictionary<string, object> parameterValues)
            {
                //SetInputParameterValues(parameterValues);
            }
        }
        
        [TestMethod]
        public void StoredMethod_Constructs_AsExpected()
        {
            var package = new Mock<IPackage>();
            var name = "Casey";

            var storedMethod = new StoredMethodStub(package.Object, name);

            Assert.AreSame(package.Object, storedMethod.Package);
            Assert.AreEqual(name, storedMethod.Name);
        }

        [TestMethod]
        public void StoredMethod_Constructs_AsExpectedXX()
        {
            var inParam1 = new Mock<IInputParameter>();
            var inParam2 = new Mock<IInputParameter>();
            var inParam3 = new Mock<IInputParameter>();
            var inParam4 = new Mock<IInputParameter>();

            var storedMethod = new StoredMethodStub();

        }
    }
}
