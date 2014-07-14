using System.Collections.Generic;
using System.Data;

using Disposable.Common.ServiceLocator;

namespace Disposable.Data.ObjectMapping
{
    public class ObjectMapper : IObjectMapper
    {
        public T GetOne<T>(DataSet dataSet) where T : class, new()
        {
            return GetOne<T>(new MapperDataTableAdapter(dataSet.Tables[0]));
        }

        public T GetOne<T>(IDataReader dataReader) where T : class, new()
        {
            return GetOne<T>(new MapperIDataReaderAdapter(dataReader));
        }

        public IEnumerable<T> GetMany<T>(DataSet dataSet) where T : class, new()
        {
            return GetMany<T>(new MapperDataTableAdapter(dataSet.Tables[0]));
        }

        public IEnumerable<T> GetMany<T>(IDataReader dataReader) where T : class, new()
        {
            return GetMany<T>(new MapperIDataReaderAdapter(dataReader)); 
        }

        private T GetOne<T>(MapperDataReader mapperDataReader) where T : class, new()
        {
            throw new System.NotImplementedException();
        }

        private IEnumerable<T> GetMany<T>(MapperDataReader mapperDataReader) where T : class, new()
        {
            throw new System.NotImplementedException();
        } 

        private static IObjectBinding<T> GetObjectBinding<T>() where T : class
        {
            IObjectBinding<T> objectBinding;

            if (!Locator.Current.TryGetInstance(out objectBinding))
            {
                objectBinding = CreateObjectBinding<T>();
            }

            return objectBinding;
        }

        private static IObjectBinding<T> CreateObjectBinding<T>() where T : class
        {
            var objectBinding = new ObjectBinding<T>();
            var locator = Locator.Current as Locator;

            if (locator != null)
            {
                locator.Register<IObjectBinding<T>>(() => objectBinding);
            }

            return objectBinding;
        }
    }
}
