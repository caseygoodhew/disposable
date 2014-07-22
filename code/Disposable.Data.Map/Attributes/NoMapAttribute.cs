using System;

namespace Disposable.Data.Map.Attributes
{
    /// <summary>
    /// Fields or Properties flagged with this attribute will not be ignored during mapping.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class NoMapAttribute : Attribute
    {
    }
}
