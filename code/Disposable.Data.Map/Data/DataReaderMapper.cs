using System;
using System.Collections.Generic;
using System.Data;

using Disposable.Common.ServiceLocator;

namespace Disposable.Data.Map.Data
{
    /// <summary>
    /// Mapper object to map to <see cref="IDataReader"/>s.
    /// </summary>
    internal class DataReaderMapper : IDataSourceMapper<IDataReader>
    {
        private readonly Lazy<IDataSourceMapper<DataSourceReader>> mapperDataReaderObjectMapper =
            new Lazy<IDataSourceMapper<DataSourceReader>>(() => Locator.Current.Instance<IDataSourceMapper<DataSourceReader>>());
        
        /// <summary>
        /// Maps exactly one record from a <see cref="IDataReader"/>.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataReader">The <see cref="IDataReader"/> to use to map the object.</param>
        /// <returns>A single object of the mapped <see cref="IDataReader"/>.</returns>
        public T GetOne<T>(IDataReader dataReader) where T : class, new()
        {
            return mapperDataReaderObjectMapper.Value.GetOne<T>(new DataReaderAdapter(dataReader));
        }

        /// <summary>
        /// Maps all available rows in a <see cref="IDataReader"/> to the object type.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataReader">The <see cref="IDataReader"/> to use to map the objects.</param>
        /// <returns>IEnumerable{T} objects of the mapped <see cref="IDataReader"/>.</returns>
        public IEnumerable<T> GetMany<T>(IDataReader dataReader) where T : class, new()
        {
            return mapperDataReaderObjectMapper.Value.GetMany<T>(new DataReaderAdapter(dataReader)); 
        }
    }
}
