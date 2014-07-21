using Disposable.Data.Map.Data;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Gets <see cref="ITypeBinding{T}"/> objects.
    /// </summary>
    internal interface ITypeBindingFactory
    {
        /// <summary>
        /// Gets an <see cref="ITypeBinding{T}"/> that is bound to the given generic type.
        /// </summary>
        /// <typeparam name="T">The generic type to bind.</typeparam>
        /// <returns>The <see cref="ITypeBinding{T}"/> for the generic type.</returns>
        ITypeBinding<T> Get<T>() where T : class;

        /// <summary>
        /// Gets an <see cref="ITypeBinding{T}"/> that is bound to the given generic type and reduced to the 
        /// relavent <see cref="IMemberBinding{TObject}"/>s that can be mapped to the to the given <see cref="DataSourceReader"/>.
        /// </summary>
        /// <typeparam name="T">The generic type to bind.</typeparam>
        /// <param name="dataSourceReader">The <see cref="DataSourceReader"/> to be used as a hint to reduce the visible <see cref="IMemberBinding{TObject}"/>s.</param>
        /// <returns>The <see cref="ITypeBinding{T}"/> for the generic type.</returns>
        ITypeBinding<T> Get<T>(DataSourceReader dataSourceReader) where T : class;
    }
}