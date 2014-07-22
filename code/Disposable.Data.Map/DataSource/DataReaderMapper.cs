using System;
using System.Collections.Generic;
using System.Data;

using Disposable.Common.ServiceLocator;

namespace Disposable.Data.Map.DataSource
{
    /// <summary>
    /// Maps a <see cref="IDataReader"/> to a generic type.
    /// </summary>
    internal class DataReaderMapper : IDataSourceMapper<IDataReader>
    {
        private readonly Lazy<IDataSourceMapper<DataSourceReader>> mapperDataReaderObjectMapper =
            new Lazy<IDataSourceMapper<DataSourceReader>>(() => Locator.Current.Instance<IDataSourceMapper<DataSourceReader>>());

        /// <summary>
        /// Maps exactly one record from a <see cref="IDataReader"/>.
        /// </summary>
        /// <typeparam name="TObject">The generic type to map to.</typeparam>
        /// <param name="dataReader">The <see cref="IDataReader"/> to use to map the object.</param>
        /// <returns>A single object generated from the mapped <see cref="IDataReader"/>.</returns>
        public TObject GetOne<TObject>(IDataReader dataReader) where TObject : class, new()
        {
            return mapperDataReaderObjectMapper.Value.GetOne<TObject>(new DataReaderAdapter(dataReader));
        }

        /// <summary>
        /// Maps all records from a <see cref="IDataReader"/>.
        /// </summary>
        /// <typeparam name="TObject">The generic type to map to.</typeparam>
        /// <param name="dataReader">The <see cref="IDataReader"/> to use to map the object.</param>
        /// <returns>Multiple object generated from the mapped <see cref="IDataReader"/>.</returns>
        public IEnumerable<TObject> GetMany<TObject>(IDataReader dataReader) where TObject : class, new()
        {
            return mapperDataReaderObjectMapper.Value.GetMany<TObject>(new DataReaderAdapter(dataReader)); 
        }
    }
}
