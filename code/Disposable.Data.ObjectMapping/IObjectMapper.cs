using System.Collections.Generic;
using System.Data;

namespace Disposable.Data.ObjectMapping
{
    /// <summary>
    /// Interface for a mapper object to map to <see cref="DataSet"/>s and <see cref="IDataReader"/>s.
    /// </summary>
    public interface IObjectMapper
    {
        /// <summary>
        /// Maps exactly one record from a <see cref="DataSet"/>.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataSet">The <see cref="DataSet"/> to use to map the object.</param>
        /// <returns>A single object of the mapped <see cref="DataSet"/>.</returns>
        T GetOne<T>(DataSet dataSet) where T : class, new();

        /// <summary>
        /// Maps exactly one record from a <see cref="IDataReader"/>.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataReader">The <see cref="IDataReader"/> to use to map the object.</param>
        /// <returns>A single object of the mapped <see cref="IDataReader"/>.</returns>
        T GetOne<T>(IDataReader dataReader) where T : class, new();

        /// <summary>
        /// Maps all available rows in a <see cref="DataSet"/> to the object type.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataSet">The <see cref="DataSet"/> to use to map the objects.</param>
        /// <returns>IEnumerable{T} objects of the mapped <see cref="DataSet"/>.</returns>
        IEnumerable<T> GetMany<T>(DataSet dataSet) where T : class, new();

        /// <summary>
        /// Maps all available rows in a <see cref="IDataReader"/> to the object type.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataReader">The <see cref="IDataReader"/> to use to map the objects.</param>
        /// <returns>IEnumerable{T} objects of the mapped <see cref="IDataReader"/>.</returns>
        IEnumerable<T> GetMany<T>(IDataReader dataReader) where T : class, new();
    }
}
