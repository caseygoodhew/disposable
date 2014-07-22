using System.Data;

namespace Disposable.Data.Map.DataSource
{
    internal interface IDataSourceReader : IDataReader
    {
        /// <summary>
        /// Advances the IDataReader to the next record.
        /// </summary>
        /// <returns>true if there are more rows; otherwise, false.</returns>
        bool InternalRead();

        /// <summary>
        /// Tries to get the index of the named field.=
        /// </summary>
        /// <param name="name">The name of the field to find.</param>
        /// <param name="ordinal">The index of the named field.</param>
        /// <returns>true if the named field was found.</returns>
        bool TryGetOrdinal(string name, out int ordinal);

        /// <summary>
        /// Return a value indicating if the name will map to an ordinal.
        /// </summary>
        /// <param name="name">The name of the field to find.</param>
        /// <returns>true if the named field was found.</returns>
        bool HasOrdinal(string name);
    }
}
