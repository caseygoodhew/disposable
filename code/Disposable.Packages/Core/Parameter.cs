using System.Data;

namespace Disposable.Packages.Core
{
    public abstract class Parameter
    {
        internal readonly string Name;

        internal readonly DbType DataType;

        protected Parameter(string name, DbType dataType)
        {
            Name = name;
            DataType = dataType;
        }
    }
}
