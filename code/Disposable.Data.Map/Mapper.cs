using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.DataSource;
using System.Collections.Generic;
using System.Data;

namespace Disposable.Data.Map
{
    /// <summary>
    /// Maps data sets and readers to generic types.
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Maps exactly one record from a <see cref="DataSet"/>.
        /// </summary>
        /// <typeparam name="TObject">The generic type to map to.</typeparam>
        /// <param name="dataSet">The <see cref="DataSet"/> to use to map the object.</param>
        /// <returns>A single object generated from the mapped <see cref="DataSet"/>.</returns>
        public static TObject GetOne<TObject>(DataSet dataSet) where TObject : class, new()
        {
            return GetOne<DataSet, TObject>(dataSet);
        }

        /// <summary>
        /// Maps exactly one record from a <see cref="IDataReader"/>.
        /// </summary>
        /// <typeparam name="TObject">The generic type to map to.</typeparam>
        /// <param name="dataReader">The <see cref="IDataReader"/> to use to map the object.</param>
        /// <returns>A single object generated from the mapped <see cref="IDataReader"/>.</returns>
        public static TObject GetOne<TObject>(IDataReader dataReader) where TObject : class, new()
        {
            return GetOne<IDataReader, TObject>(dataReader);
        }

        /// <summary>
        /// Maps all records from a <see cref="DataSet"/>.
        /// </summary>
        /// <typeparam name="TObject">The generic type to map to.</typeparam>
        /// <param name="dataSet">The <see cref="DataSet"/> to use to map the object.</param>
        /// <returns>Multiple object generated from the mapped <see cref="DataSet"/>.</returns>
        public static IEnumerable<TObject> GetMany<TObject>(DataSet dataSet) where TObject : class, new()
        {
            return GetMany<DataSet, TObject>(dataSet);
        }

        /// <summary>
        /// Maps all records from a <see cref="IDataReader"/>.
        /// </summary>
        /// <typeparam name="TObject">The generic type to map to.</typeparam>
        /// <param name="dataReader">The <see cref="IDataReader"/> to use to map the object.</param>
        /// <returns>Multiple object generated from the mapped <see cref="IDataReader"/>.</returns>
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
