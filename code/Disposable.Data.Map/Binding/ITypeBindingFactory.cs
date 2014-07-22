using Disposable.Data.Map.DataSource;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Interface for implementations which provide <see cref="ITypeBinding{T}"/> instances.
    /// </summary>
    internal interface ITypeBindingFactory
    {
        /// <summary>
        /// Gets an <see cref="ITypeBinding{T}"/> instance that is bound to the given generic type.
        /// </summary>
        /// <typeparam name="T">The generic type to bind.</typeparam>
        /// <returns>An <see cref="ITypeBinding{T}"/> instance that is bound to the given generic type.</returns>
        ITypeBinding<T> Get<T>() where T : class;

        /// <summary>
        /// Gets an <see cref="ITypeBinding{T}"/> instance that is bound to the given generic type 
        /// and reduced to the relevant <see cref="IMemberBinding{TObject}"/>s that can possibly be 
        /// mapped to the to the given <see cref="DataSourceReader"/>.
        /// </summary>
        /// <typeparam name="T">The generic type to bind.</typeparam>
        /// <param name="dataSourceReader">The <see cref="DataSourceReader"/> to be used as a hint to reduce the visible <see cref="IMemberBinding{TObject}"/>s.</param>
        /// <returns>A reduced <see cref="ITypeBinding{T}"/> instance that is bound to the given generic type.</returns>
        ITypeBinding<T> Get<T>(DataSourceReader dataSourceReader) where T : class;
    }
}