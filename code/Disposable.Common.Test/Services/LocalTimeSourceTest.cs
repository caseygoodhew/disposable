using Disposable.Common.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace Disposable.Common.Test.Services
{
    [TestClass]
    public class LocalTimeSourceTest
    {
        [TestMethod]
        public void LocalTimeSource_Now_ReturnsDateTimeUtcNow()
        {
            ITimeSource timeSource = new LocalTimeSource();
            
            var now =  DateTime.UtcNow;
            var timeSourceNow = timeSource.Now;

            // we can't expect them to be equal as they're potentially captured at 
            // different tick counts, but within 100 ms of each other is close enough.
            Assert.IsTrue(now <= timeSourceNow);
            Assert.IsTrue((timeSourceNow <= now.Add(new TimeSpan(0,0,0,0, 100))));
        }
    }
}
