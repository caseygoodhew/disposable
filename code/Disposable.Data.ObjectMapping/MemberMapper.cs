using System;
using System.Linq;
using System.Reflection;

using Disposable.Data.ObjectMapping.Attributes;

namespace Disposable.Data.ObjectMapping
{
    /// <summary>
    /// Minimally decorated MemberInfo instances.
    /// </summary>
    /// <typeparam name="TObject">The member owner type.</typeparam>
    internal class MemberMapper<TObject> : IMemberMapper<TObject> where TObject : class
    {
        private readonly Action<TObject, object> valueSetter;

        private readonly FieldInfo fieldInfo;
        
        private readonly PropertyInfo propertyInfo;

        private readonly Type dataType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberMapper{TObject}"/> class.
        /// </summary>
        /// <param name="memberInfo">The underlying <see cref="MemberInfo"/>.</param>
        internal MemberMapper(MemberInfo memberInfo)
        {
            if (memberInfo is FieldInfo)
            {
                valueSetter = FieldValueSetter;
                fieldInfo = memberInfo as FieldInfo;
                dataType = fieldInfo.FieldType;
            }
            else if (memberInfo is PropertyInfo)
            {
                valueSetter = PropertyValueSetter;
                propertyInfo = memberInfo as PropertyInfo;
                dataType = propertyInfo.PropertyType;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

            MemberName = ReadMapAsAttribute(memberInfo) ?? memberInfo.Name;
        }

        /// <summary>
        /// Gets the member name.
        /// </summary>
        public string MemberName { get; private set; }

        /// <summary>
        /// Sets the value of the member against the given object.
        /// </summary>
        /// <param name="obj">The object to set the member value against.</param>
        /// <param name="value">The value to set.</param>
        public void SetValue(TObject obj, object value)
        {
            valueSetter.Invoke(obj, ConvertTo(value, dataType));
        }

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
        
        private void FieldValueSetter(TObject obj, object value)
        {
            fieldInfo.SetValue(obj, value);
        }

        private void PropertyValueSetter(TObject obj, object value)
        {
            propertyInfo.SetValue(obj, value);
        }
    }
}
