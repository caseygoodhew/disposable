using System.Collections.Generic;

using Disposable.Data.Map.Data;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Interface for enumerating type <see cref="IMemberBinding{TObject}"/>s and interacting with object level member mapping.
    /// </summary>
    /// <typeparam name="TObject">The type to interact with.</typeparam>
    internal interface ITypeBinding<in TObject> : IEnumerable<IMemberBinding<TObject>> where TObject : class
    {
        /// <summary>
        /// Called before any data is automatically mapped against the object.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="dataSourceReader">The <see cref="DataSourceReader"/> that contains the data to map.</param>
        void BeginMapping(TObject obj, DataSourceReader dataSourceReader);

        /// <summary>
        /// Called after all data is automatically mapped against the object.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="dataSourceReader">The <see cref="DataSourceReader"/> that contains the data to map.</param>
        void EndMapping(TObject obj, DataSourceReader dataSourceReader);
    }
}
