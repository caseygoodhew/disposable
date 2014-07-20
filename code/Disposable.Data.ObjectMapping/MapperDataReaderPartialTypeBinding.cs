using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Disposable.Data.ObjectMapping
{
    /// <summary>
    /// Provides partial type mapping services and <see cref="IMemberMapper{TObject}"/> enumeration based on which methods can actually be mapped to <see cref="MapperDataReader"/> ordinals.
    /// </summary>
    /// <typeparam name="TObject">The type to map.</typeparam>
    internal class MapperDataReaderPartialTypeBinding<TObject> : ITypeBinding<TObject> where TObject : class
    {
        private readonly IEnumerable<IMemberMapper<TObject>> members;

        private readonly ITypeBinding<TObject> underlyingTypeBinding;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperDataReaderPartialTypeBinding{TObject}"/> class.
        /// </summary>
        /// <param name="sourceTypeBinding">An <see cref="ITypeBinding{TObject}"/> instance that provides a binding source.</param>
        /// <param name="mapperDataReader">The <see cref="MapperDataReader"/> to bind against.</param>
        internal MapperDataReaderPartialTypeBinding(ITypeBinding<TObject> sourceTypeBinding, MapperDataReader mapperDataReader)
        {
            members = sourceTypeBinding.Where(x => mapperDataReader.HasOrdinal(x.MemberName));
            underlyingTypeBinding = sourceTypeBinding;
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<IMemberMapper<TObject>> GetEnumerator()
        {
            return members.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Called before any data is automatically mapped against the object.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="mapperDataReader">The <see cref="MapperDataReader"/> that contains the data to map.</param>
        public void BeginMapping(TObject obj, MapperDataReader mapperDataReader)
        {
            underlyingTypeBinding.BeginMapping(obj, mapperDataReader);
        }

        /// <summary>
        /// Called after all data is automatically mapped against the object.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="mapperDataReader">The <see cref="MapperDataReader"/> that contains the data to map.</param>
        public void EndMapping(TObject obj, MapperDataReader mapperDataReader)
        {
            underlyingTypeBinding.EndMapping(obj, mapperDataReader);
        }
    }
}
