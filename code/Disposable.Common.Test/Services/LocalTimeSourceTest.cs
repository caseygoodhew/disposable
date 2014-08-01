using System;
using System.Linq;

using Disposable.Common.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Common.Test.Services
{
    [TestClass]
    public class LocalTimeSourceTest
    {
        [TestMethod]
        public void LocalTimeSource_Now_ReturnsDateTimeNow()
        {
            ITimeSource timeSource = new LocalTimeSource();
            
            var now =  DateTime.Now;
            var timeSourceNow = timeSource.Now;

            // we can't expect them to be equal as they're potentially captured at 
            // different tick counts, but within 100 ms of each other is close enough.
            Assert.IsTrue(now <= timeSourceNow);
            Assert.IsTrue((timeSourceNow <= now.Add(new TimeSpan(0,0,0,0, 100))));
        }
    }
}
