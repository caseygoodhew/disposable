using System;
using System.Collections.Generic;
using System.Data;

using Disposable.Common.ServiceLocator;

namespace Disposable.Data.Map.DataSource
{
    /// <summary>
    /// Maps a <see cref="DataSet"/> to a generic type.
    /// </summary>
    internal class DataSetMapper : IDataSourceMapper<DataSet>
    {
        private readonly Lazy<IDataSourceMapper<IDataSourceReader>> dataSourceReaderMapper =
            new Lazy<IDataSourceMapper<IDataSourceReader>>(() => Locator.Current.Instance<IDataSourceMapper<IDataSourceReader>>());
        
        /// <summary>
        /// Maps exactly one record from a <see cref="DataSet"/>.
        /// </summary>
        /// <typeparam name="TObject">The generic type to map to.</typeparam>
        /// <param name="dataSet">The <see cref="DataSet"/> to use to map the object.</param>
        /// <returns>A single object generated from the mapped <see cref="DataSet"/>.</returns>
        public TObject GetOne<TObject>(DataSet dataSet) where TObject : class, new()
        {
            return dataSourceReaderMapper.Value.GetOne<TObject>(new DataTableAdapter(dataSet.Tables[0]));
        }

        /// <summary>
        /// Maps all records from a <see cref="DataSet"/>.
        /// </summary>
        /// <typeparam name="TObject">The generic type to map to.</typeparam>
        /// <param name="dataSet">The <see cref="DataSet"/> to use to map the object.</param>
        /// <returns>Multiple object generated from the mapped <see cref="DataSet"/>.</returns>
        public IEnumerable<TObject> GetMany<TObject>(DataSet dataSet) where TObject : class, new()
        {
            return dataSourceReaderMapper.Value.GetMany<TObject>(new DataTableAdapter(dataSet.Tables[0]));
        }
    }
}
