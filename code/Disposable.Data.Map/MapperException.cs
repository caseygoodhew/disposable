using System;

using Disposable.Data.Map.Data;

namespace Disposable.Data.Map
{
    /// <summary>
    /// <see cref="IDataSourceMapper{TDataSource}"/> exception.
    /// </summary>
    [Serializable]
    public sealed class MapperException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public MapperException(string message) : base(message)
        {
        }
    }
}
