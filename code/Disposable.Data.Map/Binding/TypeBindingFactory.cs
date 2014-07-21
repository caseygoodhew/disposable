using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Data;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Factory to create to cache and retrieve <see cref="ITypeBinding{T}"/>s.
    /// </summary>
    internal class TypeBindingFactory : ITypeBindingFactory
    {
        /// <summary>
        /// Gets an <see cref="ITypeBinding{T}"/> that is bound to the given generic type.
        /// </summary>
        /// <typeparam name="T">The generic type to bind.</typeparam>
        /// <returns>The <see cref="ITypeBinding{T}"/> for the generic type.</returns>
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
        /// Gets an <see cref="ITypeBinding{T}"/> that is bound to the given generic type and reduced to the 
        /// relavent <see cref="IMemberBindingBinding{TObject}"/>s that can be mapped to the to the given <see cref="DataSourceReader"/>.
        /// </summary>
        /// <typeparam name="T">The generic type to bind.</typeparam>
        /// <param name="dataSourceReader">The <see cref="DataSourceReader"/> to be used as a hint to reduce the visible <see cref="IMemberBindingBinding{TObject}"/>s.</param>
        /// <returns>The <see cref="ITypeBinding{T}"/> for the generic type.</returns>
        public ITypeBinding<T> Get<T>(DataSourceReader dataSourceReader) where T : class
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