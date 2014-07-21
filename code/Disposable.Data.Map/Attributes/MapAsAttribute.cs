using System;

namespace Disposable.Data.Map.Attributes
{
    /// <summary>
    /// Fields or Properties flagged with this attribute can specify an alternative mapping name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class MapAsAttribute : Attribute
    {
        /// <summary>
        /// The <see cref="Name"/> provided at initialization.
        /// </summary>
        public readonly string Name;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MapAsAttribute"/> class.
        /// </summary>
        /// <param name="name">The member name to use for automatic object mapping.</param>
        public MapAsAttribute(string name)
        {
            Name = name;
        }
    }
}
