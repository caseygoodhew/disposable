using System;
using System.Linq;
using System.Reflection;

using Disposable.Data.ObjectMapping.Attributes;

namespace Disposable.Data.ObjectMapping
{
    internal class MemberMapper<TObject> : IMemberMapper<TObject> where TObject : class
    {
        public string MemberName { get; private set; }

        private readonly Action<TObject, object> valueSetter;

        private readonly FieldInfo fieldInfo;
        
        private readonly PropertyInfo propertyInfo;

        private readonly Type dataType;

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

        public void SetValue(TObject obj, object value)
        {
            valueSetter.Invoke(obj, ConvertTo(value, dataType));
        }

        private void FieldValueSetter(TObject obj, object value)
        {
            fieldInfo.SetValue(obj, value);
        }

        private void PropertyValueSetter(TObject obj, object value)
        {
            propertyInfo.SetValue(obj, value);
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
    }
}
