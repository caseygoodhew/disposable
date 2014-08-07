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
    public class StoredMethodInstanceTest
    {
        public class UnderlyingDatabaseExceptionStub : UnderlyingDatabaseException 
        {
            public UnderlyingDatabaseExceptionStub() : base(new Exception())
            {
            }
        }

        [TestMethod]
        public void StoredMethodInstance_Constructor_Succeeds()
        {
            var package = new Mock<IPackage>().Object;
            var name = "Any name";

            var inputParameters = Enumerable.Empty<IInputParameter>();
            var outputParameters = Enumerable.Empty<IOutputParameter>();

            var storedMethodMock = new Mock<IStoredMethod>(MockBehavior.Strict);
            storedMethodMock.SetupGet(x => x.Package).Returns(package);
            storedMethodMock.SetupGet(x => x.Name).Returns(name);

            new StoredMethodInstance(storedMethodMock.Object, inputParameters, outputParameters);
            new StoredMethodInstance(storedMethodMock.Object, inputParameters);
            new StoredMethodInstance(storedMethodMock.Object, outputParameters);
            var instance = new StoredMethodInstance(storedMethodMock.Object);

            Assert.AreSame(package, instance.Package);
            Assert.AreEqual(name, instance.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StoredMethodInstance_ConstructorWithNullStoredMethod_Throws()
        {
            new StoredMethodInstance(null);
        }
        
        [TestMethod]
        public void Handle_Calls_StoredMethod()
        {
            var storedMethodMock = new Mock<IStoredMethod>(MockBehavior.Strict);
            var exceptionDescription = new ExceptionDescription(string.Empty);
            var underlyingDatabaseException = new UnderlyingDatabaseExceptionStub();

            var instance = new StoredMethodInstance(storedMethodMock.Object);

            storedMethodMock.Setup(
                x =>
                x.Handle(
                    It.Is<IStoredMethodInstance>(p => p == instance),
                    It.Is<ExceptionDescription>(p => p == exceptionDescription),
                    It.Is<UnderlyingDatabaseException>(p => p == underlyingDatabaseException)))
                .Returns(exceptionDescription)
                .Verifiable();

            instance.Handle(exceptionDescription, underlyingDatabaseException);

            storedMethodMock.Verify();
        }

        [TestMethod]
        public void GetsAndSets_IParameterValues_AsExpected()
        {
            var inputParamName1 = "william";
            var inputParamValue1 = 'x';
            var inputParamName2 = "casey";
            var inputParamValue2 = 99;
            var inputParamName3 = "goodhew";
            var inputParamValue3 = 3.14;
            var outputParamName1 = "yesac";
            var outputParamName2 = "wehdoog";
            var outputParamValue2 = "result";

            var inputParam1 = new Mock<IInputParameter>();
            inputParam1.SetupGet(x => x.Name).Returns(inputParamName1);

            var inputParam2 = new Mock<IInputParameter>();
            inputParam2.SetupGet(x => x.Name).Returns(inputParamName2);

            var inputParam3 = new Mock<IInputParameter>();
            inputParam3.SetupGet(x => x.Name).Returns(inputParamName3);

            var outputParam1 = new Mock<IOutputParameter>();
            outputParam1.SetupGet(x => x.Name).Returns(outputParamName1);

            var outputParam2 = new Mock<IOutputParameter>();
            outputParam2.SetupGet(x => x.Name).Returns(outputParamName2);

            var storedMethod = new Mock<IStoredMethod>().Object;

            var instance = new StoredMethodInstance(
                storedMethod,
                new[] { inputParam1.Object, inputParam2.Object, inputParam3.Object },
                new[] { outputParam1.Object, outputParam2.Object });

            Assert.AreEqual(0, instance.GetValues<IInputParameterValue>(false).Count);
            Assert.AreEqual(0, instance.GetValues<IOutputParameterValue>(false).Count);

            var inputGetResult1 = instance.GetValues<IInputParameterValue>(true);
            Assert.AreEqual(3, inputGetResult1.Count);
            Assert.AreSame(inputParam1.Object, inputGetResult1.First().AsInputParameter());
            Assert.AreSame(inputParam2.Object, inputGetResult1.Skip(1).First().AsInputParameter());
            Assert.AreSame(inputParam3.Object, inputGetResult1.Skip(2).First().AsInputParameter());

            var outputGetResult1 = instance.GetValues<IOutputParameterValue>(true);
            Assert.AreEqual(2, outputGetResult1.Count);
            Assert.AreSame(outputParam1.Object, outputGetResult1.First().AsOutputParameter());
            Assert.AreSame(outputParam2.Object, outputGetResult1.Skip(1).First().AsOutputParameter());
            
            instance.SetValue<IInputParameterValue>(inputParamName1, inputParamValue1);
            var inputGetResult2 = instance.GetValues<IInputParameterValue>(false);
            Assert.AreEqual(1, inputGetResult2.Count);
            Assert.AreEqual(inputParamName1, inputGetResult2.First().Name);
            Assert.AreEqual(inputParamValue1, inputGetResult2.First().Value);

            instance.SetValue<IOutputParameterValue>(outputParamName2, outputParamValue2);
            var outputGetResult2 = instance.GetValues<IOutputParameterValue>(false);
            Assert.AreEqual(1, outputGetResult2.Count);
            Assert.AreEqual(outputParamName2, outputGetResult2.First().Name);
            Assert.AreEqual(outputParamValue2, outputGetResult2.First().Value);

            instance.SetValues<IInputParameterValue>(new Dictionary<string, object>
                                                         {
                                                             {inputParamName2, inputParamValue2},
                                                             {inputParamName3, inputParamValue3}
                                                         });

            var inputGetResult3 = instance.GetValues<IInputParameterValue>(false);
            Assert.AreEqual(3, inputGetResult3.Count);
            Assert.AreEqual(inputParamName1, inputGetResult3.First().Name);
            Assert.AreEqual(inputParamValue1, inputGetResult3.First().Value);
            Assert.AreEqual(inputParamName2, inputGetResult3.Skip(1).First().Name);
            Assert.AreEqual(inputParamValue2, inputGetResult3.Skip(1).First().Value);
            Assert.AreEqual(inputParamName3, inputGetResult3.Skip(2).First().Name);
            Assert.AreEqual(inputParamValue3, inputGetResult3.Skip(2).First().Value);

            Assert.AreEqual(inputParamValue2, instance.GetValue<IInputParameterValue>(inputParamName2).Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetValue_TwoTimes_Throws()
        {
            var inputParamName = "william";
            var inputParamValue = 'x';
            
            var inputParam1 = new Mock<IInputParameter>();
            inputParam1.SetupGet(x => x.Name).Returns(inputParamName);
            
            var storedMethod = new Mock<IStoredMethod>().Object;

            var instance = new StoredMethodInstance(storedMethod, new[] { inputParam1.Object });

            instance.SetValue<IInputParameterValue>(inputParamName, inputParamValue);
            Assert.AreEqual(inputParamValue, instance.GetValue<IInputParameterValue>(inputParamName).Value);
            instance.SetValue<IInputParameterValue>(inputParamName, inputParamValue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetValues_UsingIParameterValueGenericType_Throws()
        {
            var storedMethod = new Mock<IStoredMethod>().Object;

            var instance = new StoredMethodInstance(storedMethod);

            instance.GetValues<IParameterValue>(true);
        }
    }
}
