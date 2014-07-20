using System.Collections.Generic;

namespace Disposable.Data.ObjectMapping
{
    /// <summary>
    /// Interface for enumerating type <see cref="IMemberMapper{TObject}"/>s and interacting with object level member mapping.
    /// </summary>
    /// <typeparam name="TObject">The type to interact with.</typeparam>
    internal interface ITypeBinding<TObject> : IEnumerable<IMemberMapper<TObject>> where TObject : class
    {
        /// <summary>
        /// Called before any data is automatically mapped against the object.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="mapperDataReader">The <see cref="MapperDataReader"/> that contains the data to map.</param>
        void BeginMapping(TObject obj, MapperDataReader mapperDataReader);

        /// <summary>
        /// Called after all data is automatically mapped against the object.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="mapperDataReader">The <see cref="MapperDataReader"/> that contains the data to map.</param>
        void EndMapping(TObject obj, MapperDataReader mapperDataReader);
    }
}
