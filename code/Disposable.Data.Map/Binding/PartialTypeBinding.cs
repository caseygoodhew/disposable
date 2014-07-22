using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Disposable.Data.Map.DataSource;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Generic type binding and mapping decoration with partial <see cref="IMemberBinding{TObject}"/> enumeration.
    /// </summary>
    /// <typeparam name="TObject">The type to bind to.</typeparam>
    internal class PartialTypeBinding<TObject> : ITypeBinding<TObject> where TObject : class
    {
        private readonly IEnumerable<IMemberBinding<TObject>> members;

        private readonly ITypeBinding<TObject> underlyingTypeBinding;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PartialTypeBinding{TObject}"/> class.
        /// </summary>
        /// <param name="sourceTypeBinding">An <see cref="ITypeBinding{TObject}"/> instance that provides a binding source.</param>
        /// <param name="dataSourceReader">The <see cref="IDataSourceReader"/> to reduce against.</param>
        internal PartialTypeBinding(ITypeBinding<TObject> sourceTypeBinding, IDataSourceReader dataSourceReader)
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
        /// <param name="dataSourceReader">The <see cref="IDataSourceReader"/> that contains the data to map.</param>
        public void BeginMapping(TObject obj, IDataSourceReader dataSourceReader)
        {
            underlyingTypeBinding.BeginMapping(obj, dataSourceReader);
        }

        /// <summary>
        /// Called after all data is automatically mapped against the object.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="dataSourceReader">The <see cref="IDataSourceReader"/> that contains the data to map.</param>
        public void EndMapping(TObject obj, IDataSourceReader dataSourceReader)
        {
            underlyingTypeBinding.EndMapping(obj, dataSourceReader);
        }
    }
}
