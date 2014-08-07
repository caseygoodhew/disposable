using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Common.Test
{
    [TestClass]
    public class ApplicationTest
    {
        [TestMethod]
        public void Application_Creates_AsExpected()
        {
            var name = "Casey";
            var description = "Goodhew";

            var application = new Application(name, description);

            Assert.AreEqual(name, application.Name);
            Assert.AreEqual(description, application.Description);

            Guard.ArgumentIsType<IApplication>(application, "application");
        }
    }
}
