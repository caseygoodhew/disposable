using System.Reflection;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Minimally decorated MemberInfo instances.
    /// </summary>
    /// <typeparam name="TObject">The member owner type.</typeparam>
    internal class PropertyBinding<TObject> : MemberBinding<TObject> where TObject : class
    {
        private readonly PropertyInfo propertyInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberBinding{TObject}"/> class.
        /// </summary>
        /// <param name="propertyInfo">The underlying <see cref="PropertyInfo"/>.</param>
        internal PropertyBinding(PropertyInfo propertyInfo) : base(propertyInfo, propertyInfo.PropertyType)
        {
            this.propertyInfo = propertyInfo;
        }

        /// <summary>
        /// Sets the value of the member against the given object.
        /// </summary>
        /// <param name="obj">The object to set the member value against.</param>
        /// <param name="value">The value to set.</param>
        public override void SetValue(TObject obj, object value)
        {
            propertyInfo.SetValue(obj, value);
        }
    }
}
