using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.DataSource;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Factory to create, cache and retrieve <see cref="ITypeBinding{T}"/> instances.
    /// </summary>
    internal class TypeBindingFactory : ITypeBindingFactory
    {
        /// <summary>
        /// Gets an <see cref="ITypeBinding{T}"/> instance that is bound to the given generic type.
        /// </summary>
        /// <typeparam name="T">The generic type to bind.</typeparam>
        /// <returns>An <see cref="ITypeBinding{T}"/> instance that is bound to the given generic type.</returns>
        public ITypeBinding<T> Get<T>() where T : class
        {
            ITypeBinding<T> typeBinding;

            if (!Locator.Current.TryGetInstance(out typeBinding))
            {
                typeBinding = Create<T>();
            }

            return typeBinding;
        }

        /// <summary>
        /// Gets an <see cref="ITypeBinding{T}"/> instance that is bound to the given generic type 
        /// and reduced to the relevant <see cref="IMemberBinding{TObject}"/>s that can possibly be 
        /// mapped to the to the given <see cref="IDataSourceReader"/>.
        /// </summary>
        /// <typeparam name="T">The generic type to bind.</typeparam>
        /// <param name="dataSourceReader">The <see cref="IDataSourceReader"/> to be used as a hint to reduce the visible <see cref="IMemberBinding{TObject}"/>s.</param>
        /// <returns>A reduced <see cref="ITypeBinding{T}"/> instance that is bound to the given generic type.</returns>
        public ITypeBinding<T> Get<T>(IDataSourceReader dataSourceReader) where T : class
        {
            return new PartialTypeBinding<T>(Get<T>(), dataSourceReader);
        }

        private static ITypeBinding<T> Create<T>() where T : class
        {
            var objectBinding = new TypeBinding<T>();
            var locator = Locator.Current as Locator;

            if (locator != null)
            {
                locator.Register<ITypeBinding<T>>(() => objectBinding);
            }

            return objectBinding;
        }
    }
}