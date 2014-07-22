using System.Reflection;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Interface to create IMemberBinding implementations.
    /// </summary>
    internal interface IMemberBindingFactory
    {
        /// <summary>
        /// Gets an <see cref="IMemberBinding{TObject}"/> instance for the given <see cref="MemberInfo"/>.
        /// </summary>
        /// <typeparam name="TObject">The generic type that owns the member.</typeparam>
        /// <param name="memberInfo">The <see cref="MemberInfo"/> of the member.</param>
        /// <returns>An <see cref="IMemberBinding{TObject}"/> instance for the given <see cref="MemberInfo"/>.</returns>
        IMemberBinding<TObject> Get<TObject>(MemberInfo memberInfo) where TObject : class;
    }
}
