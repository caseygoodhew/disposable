using System.Collections.Generic;

namespace Disposable.Data.Map.DataSource
{
    /// <summary>
    /// Interface to map a data source to a generic type.
    /// </summary>
    /// <typeparam name="TDataSource">The type of data source that will is implemented.</typeparam>
    internal interface IDataSourceMapper<in TDataSource> where TDataSource : class
    {
        /// <summary>
        /// Maps exactly one record from a data source.
        /// </summary>
        /// <typeparam name="TObject">The generic type to map to.</typeparam>
        /// <param name="dataSource">The data source to use to map the object.</param>
        /// <returns>A single object generated from the mapped data source.</returns>
        TObject GetOne<TObject>(TDataSource dataSource) where TObject : class, new();

        /// <summary>
        /// Maps all records from a data source.
        /// </summary>
        /// <typeparam name="TObject">The generic type to map to.</typeparam>
        /// <param name="dataSource">The data source to use to map the object.</param>
        /// <returns>Multiple object generated from the mapped data source.</returns>
        IEnumerable<TObject> GetMany<TObject>(TDataSource dataSource) where TObject : class, new();
    }
}
