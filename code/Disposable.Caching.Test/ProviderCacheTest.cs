using System;
using System.CodeDom;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Disposable.Caching.Test
{
    [TestClass]
    public class ProviderCacheTest
    {
        public class SomeClass { }

        public class SomeOtherClass { }

        [TestMethod]
        public void ProviderCache_WithMultipleGets_GetsOnce()
        {
            var instance = new SomeClass();
            var callCount = 0;
            Func<SomeClass> provider = () =>
                {
                    callCount++;
                    return instance;
                };
            
            var cache = new ProviderCache();
            
            cache.Register(provider);
            Assert.AreEqual(0, callCount);

            Assert.AreSame(instance, cache.Get<SomeClass>());
            Assert.AreEqual(1, callCount);

            Assert.AreSame(instance, cache.Get<SomeClass>());
            Assert.AreEqual(1, callCount);
        }

        [TestMethod]
        public void ProviderCache_GetWithProvider_RegistersAndGets()
        {
            var instance = new SomeClass();
            var callCount = 0;
            Func<SomeClass> provider = () =>
            {
                callCount++;
                return instance;
            };

            var cache = new ProviderCache();

            Assert.AreSame(instance, cache.Get(provider));
            Assert.AreEqual(1, callCount);

            Assert.AreSame(instance, cache.Get(provider));
            Assert.AreEqual(1, callCount);

            Assert.AreSame(instance, cache.Get<SomeClass>());
            Assert.AreEqual(1, callCount);
        }

        [TestMethod]
        public void ProviderCache_GetWithProviderWhenAlreadyRegisteres_DoesntFetch()
        {
            var instanceOne = new SomeClass();
            var callCountOne = 0;
            Func<SomeClass> providerOne = () =>
            {
                callCountOne++;
                return instanceOne;
            };

            var instanceTwo = new SomeClass();
            var callCountTwo = 0;
            Func<SomeClass> providerTwo = () =>
            {
                callCountTwo++;
                return instanceTwo;
            };

            var cache = new ProviderCache();

            cache.Register(providerOne);
            
            Assert.AreSame(instanceOne, cache.Get<SomeClass>());
            Assert.AreEqual(1, callCountOne);

            Assert.AreSame(instanceOne, cache.Get(providerTwo));
            Assert.AreEqual(1, callCountOne);
            Assert.AreEqual(0, callCountTwo);
        }

        [TestMethod]
        public void ProviderCache_WithMultipleGetsAndExpiry_Refreshes()
        {
            var instance = new SomeClass();
            var callCount = 0;
            Func<SomeClass> provider = () =>
            {
                callCount++;
                return instance;
            };

            var cache = new ProviderCache();

            cache.Register(provider);
            Assert.AreEqual(0, callCount);

            Assert.AreSame(instance, cache.Get<SomeClass>());
            Assert.AreEqual(1, callCount);

            cache.ExpireAll();

            Assert.AreSame(instance, cache.Get<SomeClass>());
            Assert.AreEqual(2, callCount);
        }

        [TestMethod]
        public void ProviderCache_WithTypeExpiry_ExpiresOneTypeOnly()
        {
            var instanceOne = new SomeClass();
            var callCountOne = 0;
            Func<SomeClass> providerOne = () =>
            {
                callCountOne++;
                return instanceOne;
            };

            var instanceTwo = new SomeOtherClass();
            var callCountTwo = 0;
            Func<SomeOtherClass> providerTwo = () =>
            {
                callCountTwo++;
                return instanceTwo;
            };

            var cache = new ProviderCache();

            cache.Register(providerOne);
            Assert.AreEqual(0, callCountOne);

            cache.Register(providerTwo);
            Assert.AreEqual(0, callCountTwo);

            Assert.AreSame(instanceOne, cache.Get<SomeClass>());
            Assert.AreEqual(1, callCountOne);

            Assert.AreSame(instanceTwo, cache.Get<SomeOtherClass>());
            Assert.AreEqual(1, callCountTwo);

            cache.Expire<SomeClass>();

            Assert.AreSame(instanceOne, cache.Get<SomeClass>());
            Assert.AreEqual(2, callCountOne);

            Assert.AreSame(instanceTwo, cache.Get<SomeOtherClass>());
            Assert.AreEqual(1, callCountTwo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProviderCache_WithNullProvider_Throws()
        {
            var cache = new ProviderCache();

            cache.Register<SomeClass>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyRegisteredException))]
        public void ProviderCache_WithDuplicateRegistration_Throws()
        {
            var instance = new SomeClass();
            Func<SomeClass> provider = () => instance;

            var cache = new ProviderCache();

            cache.Register(provider);
            cache.Register(provider);
        }

        [TestMethod]
        [ExpectedException(typeof(NotRegisteredException))]
        public void ProviderCache_GetUnknownType_Throws()
        {
            var cache = new ProviderCache();

            cache.Get<SomeClass>();
        }

        [TestMethod]
        public void ProviderCache_THREADING_TESTS()
        {
            // these tests need to be implemented
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Multiple_ProviderCache_Isolates()
        {
            var instance = new SomeClass();
            var callCount = 0;
            Func<SomeClass> provider = () =>
            {
                callCount++;
                return instance;
            };
            
            var cacheOne = new ProviderCache();
            var cacheTwo = new ProviderCache();

            Assert.AreSame(instance, cacheOne.Get(provider));
            Assert.AreEqual(1, callCount);

            Assert.AreSame(instance, cacheOne.Get(provider));
            Assert.AreEqual(1, callCount);

            Assert.AreSame(instance, cacheTwo.Get(provider));
            Assert.AreEqual(2, callCount);

            Assert.AreSame(instance, cacheTwo.Get(provider));
            Assert.AreEqual(2, callCount);
        }
    }
}
