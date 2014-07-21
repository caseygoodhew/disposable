using System;
using System.Collections.Generic;
using System.Data;

using Disposable.Common.ServiceLocator;

namespace Disposable.Data.Map.Data
{
    /// <summary>
    /// Mapper object to map to <see cref="DataSet"/>s.
    /// </summary>
    internal class DataSetMapper : IDataSourceMapper<DataSet>
    {
        private readonly Lazy<IDataSourceMapper<DataSourceReader>> mapperDataReaderObjectMapper = 
            new Lazy<IDataSourceMapper<DataSourceReader>>(() => Locator.Current.Instance<IDataSourceMapper<DataSourceReader>>());
        
        /// <summary>
        /// Maps exactly one record from a <see cref="DataSet"/>.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataSet">The <see cref="DataSet"/> to use to map the object.</param>
        /// <returns>A single object of the mapped <see cref="DataSet"/>.</returns>
        public T GetOne<T>(DataSet dataSet) where T : class, new()
        {
            return mapperDataReaderObjectMapper.Value.GetOne<T>(new DataTableAdapter(dataSet.Tables[0]));
        }

        /// <summary>
        /// Maps all available rows in a <see cref="DataSet"/> to the object type.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataSet">The <see cref="DataSet"/> to use to map the objects.</param>
        /// <returns>IEnumerable{T} objects of the mapped <see cref="DataSet"/>.</returns>
        public IEnumerable<T> GetMany<T>(DataSet dataSet) where T : class, new()
        {
            return mapperDataReaderObjectMapper.Value.GetMany<T>(new DataTableAdapter(dataSet.Tables[0]));
        }
    }
}
