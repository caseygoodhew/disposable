using System;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Interface to bind to a type member owned by the given generic type and provide mapping decoration.
    /// </summary>
    /// <typeparam name="TObject">The member owner type.</typeparam>
    internal interface IMemberBinding<in TObject> where TObject : class
    {
        /// <summary>
        /// Gets the member data type.
        /// </summary>
        Type DataType { get; }

        /// <summary>
        /// Gets the member name;
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Sets the member value of the given object.
        /// </summary>
        /// <param name="obj">The object to set the member value against.</param>
        /// <param name="value">The value to set.</param>
        void SetValue(TObject obj, object value);
    }
}
