using System;
using System.Linq;
using System.Reflection;

using Disposable.Data.Map.Attributes;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Minimally decorated MemberInfo instances.
    /// </summary>
    /// <typeparam name="TObject">The member owner type.</typeparam>
    internal abstract class MemberBinding<TObject> : IMemberBinding<TObject> where TObject : class
    {
        protected readonly Type MemberDataType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberBinding{TObject}"/> class.
        /// </summary>
        /// <param name="memberInfo">The underlying <see cref="MemberInfo"/>.</param>
        /// <param name="memberDataType">The underlying member data type.</param>
        protected MemberBinding(MemberInfo memberInfo, Type memberDataType)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }
            
            Name = ReadMapAsAttribute(memberInfo) ?? memberInfo.Name;
            MemberDataType = memberDataType;
        }

        public static MemberBinding<TObject> Create(MemberInfo memberInfo)
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
        
        /// <summary>
        /// Gets the member name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Sets the value of the member against the given object.
        /// </summary>
        /// <param name="obj">The object to set the member value against.</param>
        /// <param name="value">The value to set.</param>
        public abstract void SetValue(TObject obj, object value);

        private static object ConvertTo(object value, Type toType)
        {
            throw new NotImplementedException();
        }

        private static string ReadMapAsAttribute(MemberInfo memberInfo)
        {
            // TODO: check attribute inheritance
            var attribute = memberInfo.GetCustomAttributes(typeof(MapAsAttribute), true).FirstOrDefault() as MapAsAttribute;

            return attribute == null ? null : attribute.Name;
        }
    }
}
