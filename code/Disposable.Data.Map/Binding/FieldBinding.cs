using System.Reflection;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Minimally decorated MemberInfo instances.
    /// </summary>
    /// <typeparam name="TObject">The member owner type.</typeparam>
    internal class FieldBinding<TObject> : MemberBinding<TObject> where TObject : class
    {
        private readonly FieldInfo fieldInfo;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberBinding{TObject}"/> class.
        /// </summary>
        /// <param name="fieldInfo">The underlying <see cref="FieldInfo"/>.</param>
        internal FieldBinding(FieldInfo fieldInfo) : base(fieldInfo, fieldInfo.FieldType)
        {
            this.fieldInfo = fieldInfo;
        }

        /// <summary>
        /// Sets the value of the member against the given object.
        /// </summary>
        /// <param name="obj">The object to set the member value against.</param>
        /// <param name="value">The value to set.</param>
        public override void SetValue(TObject obj, object value)
        {
            fieldInfo.SetValue(obj, value);
        }
    }
}
