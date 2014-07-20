using System;

namespace Disposable.Data.ObjectMapping.Attributes
{
    /// <summary>
    /// Fields or Properties flagged with this attribute will not be automatically mapped.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class NoMapAttribute : Attribute
    {
    }
}
