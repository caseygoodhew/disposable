using System;
using System.Reflection;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Create IMemberBinding implementations.
    /// </summary>
    internal class MemberBindingFactory : IMemberBindingFactory
    {
        /// <summary>
        /// Gets an <see cref="IMemberBinding{TObject}"/> instance for the given <see cref="MemberInfo"/>.
        /// </summary>
        /// <typeparam name="TObject">The generic type that owns the member.</typeparam>
        /// <param name="memberInfo">The <see cref="MemberInfo"/> of the member.</param>
        /// <returns>An <see cref="IMemberBinding{TObject}"/> instance for the given <see cref="MemberInfo"/>.</returns>
        public IMemberBinding<TObject> Get<TObject>(MemberInfo memberInfo) where TObject : class
        {
            if (memberInfo is FieldInfo)
            {
                return new FieldBinding<TObject>(memberInfo as FieldInfo);
            }

            if (memberInfo is PropertyInfo)
            {
                return new PropertyBinding<TObject>(memberInfo as PropertyInfo);
            }

            throw new ArgumentOutOfRangeException("memberInfo");
        }
    }
}
