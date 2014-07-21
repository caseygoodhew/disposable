using System.Collections.Generic;
using System.Data;

namespace Disposable.Data.Map.Data
{
    /// <summary>
    /// Interface for a mapper object to map to <see cref="DataSet"/>s and <see cref="IDataReader"/>s.
    /// </summary>
    internal interface IDataSourceMapper<in TDataSource> where TDataSource : class
    {
        /// <summary>
        /// Maps exactly one record from a <see cref="DataSet"/>.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataSource">The <see cref="TDataSource"/> to use to map the object.</param>
        /// <returns>A single object of the mapped <see cref="DataSet"/>.</returns>
        T GetOne<T>(TDataSource dataSource) where T : class, new();

        /// <summary>
        /// Maps all available rows in a <see cref="DataSet"/> to the object type.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataSource">The <see cref="TDataSource"/> to use to map the objects.</param>
        /// <returns>IEnumerable{T} objects of the mapped <see cref="DataSet"/>.</returns>
        IEnumerable<T> GetMany<T>(TDataSource dataSource) where T : class, new();
    }
}
