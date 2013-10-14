using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disposable.Security.DataAccess.Test
{
	[TestClass]
	public class UnitTests1
	{
		[TestMethod]
		public void Pass()
		{
			var someClass = new SomeClass();

			var result = someClass.TestDirect();

			Assert.AreEqual("Application", result);
		}

		[TestMethod]
		public void Pass2()
		{
			var someClass = new SomeClass();

			var result = someClass.TestDapper();

			Assert.AreEqual("Application", result);
		}
	}
}