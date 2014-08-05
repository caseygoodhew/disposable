using System;
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
        public void Handle_Calls_StoredMethod()
        {
            var storedMethodMock = new Mock<IStoredMethod>(MockBehavior.Strict);
            var exceptionDescription = new ExceptionDescription(string.Empty);
            var underlyingDatabaseException = new UnderlyingDatabaseExceptionStub();

            var instance = new StoredMethodInstance(
                storedMethodMock.Object,
                Enumerable.Empty<IInputParameter>().ToList(),
                Enumerable.Empty<IOutputParameter>().ToList());

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
    }
}
