using System.Reflection;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Binds to a Property of the given generic type and provides mapping decoration.
    /// </summary>
    /// <typeparam name="TObject">The member owner type.</typeparam>
    internal class PropertyBinding<TObject> : MemberBinding<TObject> where TObject : class
    {
        private readonly PropertyInfo propertyInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBinding{TObject}"/> class.
        /// </summary>
        /// <param name="propertyInfo">The underlying <see cref="PropertyInfo"/>.</param>
        internal PropertyBinding(PropertyInfo propertyInfo) : base(propertyInfo, propertyInfo.PropertyType)
        {
            this.propertyInfo = propertyInfo;
        }

        /// <summary>
        /// Sets the property value of the given object.
        /// </summary>
        /// <param name="obj">The object to set the property value against.</param>
        /// <param name="value">The value to set.</param>
        public override void SetValue(TObject obj, object value)
        {
            propertyInfo.SetValue(obj, value);
        }
    }
}
