using System.Collections.Generic;
using System.Data;

using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Data;

namespace Disposable.Data.Map
{
    public static class Mapper
    {
        public static TObject GetOne<TObject>(DataSet dataSet) where TObject : class, new()
        {
            return GetOne<DataSet, TObject>(dataSet);
        }

        public static TObject GetOne<TObject>(IDataReader dataReader) where TObject : class, new()
        {
            return GetOne<IDataReader, TObject>(dataReader);
        }

        public static IEnumerable<TObject> GetMany<TObject>(DataSet dataSet) where TObject : class, new()
        {
            return GetMany<DataSet, TObject>(dataSet);
        }

        public static IEnumerable<TObject> GetMany<TObject>(IDataReader dataReader) where TObject : class, new()
        {
            return GetMany<IDataReader, TObject>(dataReader);
        }

        private static TObject GetOne<TDataSource, TObject>(TDataSource dataSource)
            where TDataSource : class
            where TObject : class, new()
        {
            return GetObjectMapper<TDataSource>().GetOne<TObject>(dataSource);
        }

        private static IEnumerable<TObject> GetMany<TDataSource, TObject>(TDataSource dataSource)
            where TDataSource : class
            where TObject : class, new()
        {
            return GetObjectMapper<TDataSource>().GetMany<TObject>(dataSource);
        }

        private static IDataSourceMapper<TDataSource> GetObjectMapper<TDataSource>() where TDataSource : class
        {
            return Locator.Current.Instance<IDataSourceMapper<TDataSource>>();
        }
    }
}
