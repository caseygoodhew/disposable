using System;
using System.Web;

using Disposable.Caching;
using Disposable.Web.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Web.Caching.Test
{
    [TestClass]
    public class RequestCacheTest
    {
        [TestInitialize]
        public void Initialize()
        {
            HttpContext.Current = FakeHttpContext.Create();
        }
        
        [TestMethod]
        public void RequestCache_Current_ReturnsValidInstance()
        {
            var instance = RequestCache.Current;
            
            Assert.IsInstanceOfType(instance, typeof(RequestCache));
            Assert.AreSame(instance, RequestCache.Current);

            HttpContext.Current = FakeHttpContext.Create();

            Assert.AreNotSame(instance, RequestCache.Current);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Current_WithNullObjectInKey_Throws()
        {
            Assert.IsNotNull(RequestCache.Current);
            HttpContext.Current.Items[RequestCache.ItemKey] = null;
            var instance = RequestCache.Current;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Current_WithUnexpectedObjectInKey_Throws()
        {
            Assert.IsNotNull(RequestCache.Current);
            HttpContext.Current.Items[RequestCache.ItemKey] = new RequestCacheTest();
            var instance = RequestCache.Current;
        }

        [TestMethod]
        public void RequestCache_InterfaceMethods_CallToBase()
        {
            var providerCacheMock = new Mock<IProviderCache>();

            var requestCache = new RequestCache(providerCacheMock.Object);

            requestCache.Register(() => new RequestCacheTest());
            providerCacheMock.Verify(x => x.Register(It.IsAny<Func<RequestCacheTest>>(), It.Is<bool>(v => !v)), Times.Once);

            requestCache.Get(() => new RequestCacheTest());
            providerCacheMock.Verify(x => x.Get(It.IsAny<Func<RequestCacheTest>>(), It.Is<bool>(v => !v)), Times.Once);

            requestCache.Expire<RequestCacheTest>();
            providerCacheMock.Verify(x => x.Expire<RequestCacheTest>(), Times.Once);

            requestCache.ExpireAll();
            providerCacheMock.Verify(x => x.ExpireAll(), Times.Once);

            providerCacheMock.Verify(x => x.Register(It.IsAny<Func<RequestCacheTest>>(), It.Is<bool>(v => !v)), Times.Once);
            providerCacheMock.Verify(x => x.Get(It.IsAny<Func<RequestCacheTest>>(), It.Is<bool>(v => !v)), Times.Once);
            providerCacheMock.Verify(x => x.Expire<RequestCacheTest>(), Times.Once);
            providerCacheMock.Verify(x => x.ExpireAll(), Times.Once);
        }
    }
}
