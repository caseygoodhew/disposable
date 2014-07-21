using System;

namespace Disposable.Data.Map.Attributes
{
    /// <summary>
    /// Methods flagged with this attribute will be called before automatic object mapping occurs.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class BeginMapAttribute : Attribute
    {
    }
}
