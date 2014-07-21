using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Disposable.Data.Map.Data;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Provides partial type mapping services and <see cref="IMemberBinding{TObject}"/> enumeration based on which methods can actually be mapped to <see cref="DataSourceReader"/> ordinals.
    /// </summary>
    /// <typeparam name="TObject">The type to map.</typeparam>
    internal class PartialTypeBinding<TObject> : ITypeBinding<TObject> where TObject : class
    {
        private readonly IEnumerable<IMemberBinding<TObject>> members;

        private readonly ITypeBinding<TObject> underlyingTypeBinding;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PartialTypeBinding{TObject}"/> class.
        /// </summary>
        /// <param name="sourceTypeBinding">An <see cref="ITypeBinding{TObject}"/> instance that provides a binding source.</param>
        /// <param name="dataSourceReader">The <see cref="DataSourceReader"/> to bind against.</param>
        internal PartialTypeBinding(ITypeBinding<TObject> sourceTypeBinding, DataSourceReader dataSourceReader)
        {
            members = sourceTypeBinding.Where(x => dataSourceReader.HasOrdinal(x.Name));
            underlyingTypeBinding = sourceTypeBinding;
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<IMemberBinding<TObject>> GetEnumerator()
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
        /// <param name="dataSourceReader">The <see cref="DataSourceReader"/> that contains the data to map.</param>
        public void BeginMapping(TObject obj, DataSourceReader dataSourceReader)
        {
            underlyingTypeBinding.BeginMapping(obj, dataSourceReader);
        }

        /// <summary>
        /// Called after all data is automatically mapped against the object.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="dataSourceReader">The <see cref="DataSourceReader"/> that contains the data to map.</param>
        public void EndMapping(TObject obj, DataSourceReader dataSourceReader)
        {
            underlyingTypeBinding.EndMapping(obj, dataSourceReader);
        }
    }
}
