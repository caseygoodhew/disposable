namespace Disposable.Data.ObjectMapping
{
    /// <summary>
    /// Interface to interact with minimally decorated MemberInfo instances.
    /// </summary>
    /// <typeparam name="TObject">The member owner type.</typeparam>
    internal interface IMemberMapper<in TObject> where TObject : class
    {
        /// <summary>
        /// Gets the member name;
        /// </summary>
        string MemberName { get; }

        /// <summary>
        /// Sets the value of the member against a given object.
        /// </summary>
        /// <param name="obj">The object to set the member value against.</param>
        /// <param name="value">The value to set.</param>
        void SetValue(TObject obj, object value);
    }
}
