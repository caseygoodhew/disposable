﻿using Disposable.Data.Map.DataSource;
using System.Collections.Generic;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Interface for enumerating type <see cref="IMemberBinding{TObject}"/>s and generic type mapping decoration.
    /// </summary>
    /// <typeparam name="TObject">The type to bind to.</typeparam>
    internal interface ITypeBinding<in TObject> : IEnumerable<IMemberBinding<TObject>> where TObject : class
    {
        /// <summary>
        /// Called before automatic mapping begins.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="dataSourceReader">The <see cref="IDataSourceReader"/> that contains the data to map.</param>
        void BeginMapping(TObject obj, IDataSourceReader dataSourceReader);

        /// <summary>
        /// Called before automatic mapping completes.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="dataSourceReader">The <see cref="IDataSourceReader"/> that contains the data to map.</param>
        void EndMapping(TObject obj, IDataSourceReader dataSourceReader);
    }
}
