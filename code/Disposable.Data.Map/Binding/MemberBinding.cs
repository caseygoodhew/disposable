using Disposable.Data.Map.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Base class to bind a Member of the given generic type and provides mapping decoration.
    /// </summary>
    /// <typeparam name="TObject">The member owner type.</typeparam>
    internal abstract class MemberBinding<TObject> : IMemberBinding<TObject> where TObject : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberBinding{TObject}"/> class.
        /// </summary>
        /// <param name="memberInfo">The underlying <see cref="MemberInfo"/>.</param>
        protected MemberBinding(MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }

            if (memberInfo.DeclaringType == null)
            {
                throw new InvalidOperationException("DeclaringType not defined.");
            }

            if (!typeof(TObject).IsAssignableFrom(memberInfo.DeclaringType))
            {
                throw new InvalidOperationException(string.Format(@"{0} is not assignable from {1}.", typeof(TObject).Name, memberInfo.DeclaringType.Name));
            }
            
            Name = ReadMapAsAttribute(memberInfo) ?? memberInfo.Name;
        }

        /// <summary>
        /// Gets the member data type.
        /// </summary>
        public abstract Type DataType { get; }

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

        private static string ReadMapAsAttribute(MemberInfo memberInfo)
        {
            // TODO: check attribute inheritance
            var attribute = memberInfo.GetCustomAttributes(typeof(MapAsAttribute), true).FirstOrDefault() as MapAsAttribute;

            return attribute == null ? null : attribute.Name;
        }
    }
}
