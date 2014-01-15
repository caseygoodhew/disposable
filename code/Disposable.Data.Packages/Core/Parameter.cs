using System.Data;

namespace Disposable.Data.Packages.Core
{
    public abstract class Parameter : IParameter
    {
        public string Name { get; private set; }
        
        public DataTypes DataType { get; private set; }

        protected Parameter(string name, DataTypes dataType)
        {
            Name = name;
            DataType = dataType;
        }
    }
}
