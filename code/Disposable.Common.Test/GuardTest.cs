using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Disposable.Common.Test
{
    [TestClass]
    public class GuardTest
    {
        [TestMethod]
        public void ArgumentNotNull_WithNotNullArgument_Succeeds()
        {
            Guard.ArgumentNotNull(true, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNotNull_WithNullArgument_Throws()
        {
            Guard.ArgumentNotNull(null, string.Empty);
        }

        [TestMethod]
        public void ArgumentNotNull_WithNullArgument_UsesExpectedArgumentName()
        {
            var argumentName = "This should be the argument name.";

            try
            {
                Guard.ArgumentNotNull(null, argumentName);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual(argumentName, e.ParamName);
            }
        }

        [TestMethod]
        public void ArgumentNotNullOrEmpty_WithValidArgument_Succeeds()
        {
            Guard.ArgumentNotNullOrEmpty("x", string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNotNullOrEmpty_WithNullArgument_Throws()
        {
            Guard.ArgumentNotNullOrEmpty(null, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNotNullOrEmpty_WithEmptyArgument_Throws()
        {
            Guard.ArgumentNotNullOrEmpty(string.Empty, string.Empty);
        }

        [TestMethod]
        public void ArgumentNotNullOrEmpty_WithNullArgument_UsesExpectedArgumentName()
        {
            var argumentName = "This should be the argument name.";

            try
            {
                Guard.ArgumentNotNullOrEmpty(null, argumentName);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual(argumentName, e.ParamName);
            }
        }

        [TestMethod]
        public void ArgumentNotNullOrEmpty_WithEmptyArgument_UsesExpectedArgumentName()
        {
            var argumentName = "This should be the argument name.";

            try
            {
                Guard.ArgumentNotNullOrEmpty(string.Empty, argumentName);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual(argumentName, e.ParamName);
            }
        }

        [TestMethod]
        public void ArgumentIsType_WithValidArgument_Succeeds()
        {
            Guard.ArgumentIsType<GuardTest>(this, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentIsType_WithNullArgument_Throws()
        {
            Guard.ArgumentIsType<GuardTest>(null, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentIsType_WithInvalidArgument_Throws()
        {
            Guard.ArgumentIsType<GuardTest>(string.Empty, string.Empty);
        }

        [TestMethod]
        public void ArgumentIsType_WithInvalidArgument_UsesExpectedArgumentName()
        {
            var argumentName = "This should be the argument name.";

            try
            {
                Guard.ArgumentIsType<GuardTest>(string.Empty, argumentName);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(argumentName, e.ParamName);
            }
        }
    }
}
