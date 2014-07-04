using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Disposable.Common.Extensions;
using Disposable.Common.ServiceLocator;
using Disposable.Data.ObjectMapping;

namespace Disposable.Data.Access.Database
{
    internal abstract class DataObjectConverter : IDataObjectConverter
    {
        private readonly static Lazy<IObjectMapper> ObjectMapper = new Lazy<IObjectMapper>(
            () => Locator.Current.Instance<IObjectMapper>());
        
        public T ConvertTo<T>(IEnumerable<object> values)
        {
            var typeT = typeof(T);

            if (typeT == typeof(DataSet))
            {
                return (T)(object)(ToDataSet(values));
            }

            if (typeT == typeof(IDataReader))
            {
                return (T)ToIDataReader(values);
            }

            if (typeT == typeof(IEnumerable<IDataReader>))
            {
                return (T)ToIDataReaders(values);
            }

            if (typeof(IEnumerable<IDataReader>).IsAssignableFrom(typeT))
            {
                throw new ArgumentException("IDataReader collections can only be returned as IEnumerable");
            }

            if (typeT.IsClass)
            {
                return InvokeObjectMapper<T>(values);
            }

            return (T)(values.Single());
        }

        protected abstract DataSet ToDataSet(IEnumerable<object> values);

        protected abstract IDataReader ToIDataReader(IEnumerable<object> values);

        protected abstract IEnumerable<IDataReader> ToIDataReaders(IEnumerable<object> values);

        private T InvokeObjectMapper<T>(IEnumerable<object> values)
        {
            var typeT = typeof(T);
            var typeToBind = typeT;
            var isEnumerable = false;

            if (typeT.ImplementsIEnumerable())
            {
                if (!typeT.IsIEnumerable())
                {
                    var substring = typeT.Name.Substring(0, typeT.Name.IndexOf('`'));
                    throw new InvalidOperationException(String.Format("Cannot convert result to specific enumerable type {0}<>. Change calling declaration to IEnumerable<>.", substring));
                }

                isEnumerable = true;
                typeToBind = typeT.GetGenericArguments()[0];
            }
            
            var mapper = ObjectMapper.Value;
            
            var method = mapper.GetType()
                               .GetMethod(isEnumerable ? "GetMany" : "GetOne", new[] { typeof(IDataReader) })
                               .MakeGenericMethod(typeToBind);

            return (T)method.Invoke(mapper, new object[] { ToIDataReader(values) });
        }
    }
}
