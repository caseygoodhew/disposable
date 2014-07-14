using System;

namespace Disposable.Data.ObjectMapping.Attributes
{
    public sealed class MapAsAttribute : Attribute
    {
        internal readonly string Name;
        
        public MapAsAttribute(string name)
        {
            Name = name;
        }
    }
}
