using System.Reflection;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Binds to a Field of the given generic type and provides mapping decoration.
    /// </summary>
    /// <typeparam name="TObject">The member owner type.</typeparam>
    internal class FieldBinding<TObject> : MemberBinding<TObject> where TObject : class
    {
        private readonly FieldInfo fieldInfo;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldBinding{TObject}"/> class.
        /// </summary>
        /// <param name="fieldInfo">The underlying <see cref="FieldInfo"/>.</param>
        internal FieldBinding(FieldInfo fieldInfo) : base(fieldInfo, fieldInfo.FieldType)
        {
            this.fieldInfo = fieldInfo;
        }

        /// <summary>
        /// Sets the field value of the given object.
        /// </summary>
        /// <param name="obj">The object to set the field value against.</param>
        /// <param name="value">The value to set.</param>
        public override void SetValue(TObject obj, object value)
        {
            fieldInfo.SetValue(obj, value);
        }
    }
}
