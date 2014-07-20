using System;

namespace Disposable.Data.ObjectMapping.Attributes
{
    /// <summary>
    /// Methods flagged with this attribute will be called after automatic object mapping occurs.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class EndMappingAttribute : Attribute
    {
    }
}
