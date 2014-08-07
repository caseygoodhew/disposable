using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Binding;
using Disposable.Data.Map.DataSource;
using Disposable.Test.Extensions;
using Disposable.Test.Runners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;

namespace Disposable.Data.Map.Test
{
    [TestClass]
    public class RegistrationTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Locator.Current.ResetRegsitrars();
        }
        
        [TestMethod]
        public void VerifyRegisters()
        {
            RegistrationRunner.VerifyRegisters(
                new List<Type>
                    {
                        typeof(ITypeBindingFactory),
                        typeof(IMemberBindingFactory),
                        typeof(IDataSourceMapper<DataSet>),
                        typeof(IDataSourceMapper<IDataReader>),
                        typeof(IDataSourceMapper<IDataSourceReader>),
                    },
                Registration.Register
            );
        }
    }
}
