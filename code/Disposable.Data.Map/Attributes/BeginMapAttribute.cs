using System;

namespace Disposable.Data.Map.Attributes
{
    /// <summary>
    /// Methods flagged with this attribute will be called before data mapping occurs. TODO: complete expected signature
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class BeginMapAttribute : Attribute
    {
    }
}
