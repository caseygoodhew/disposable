using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Disposable.Common
{
    public static class Reflection
    {
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda) where TSource : class
        {
            var type = typeof(TSource);

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' is not a property.", propertyLambda));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' is not a property.", propertyLambda));
            }

            if (propInfo.ReflectedType != null
                && type != propInfo.ReflectedType
                && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(
                    string.Format(
                        "Expression '{0}' refers to a property that is not from type {1}.",
                        propertyLambda,
                        type));
            }

            return propInfo;
        }
    }
}
