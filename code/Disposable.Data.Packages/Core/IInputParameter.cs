using System.Data;

namespace Disposable.Data.Packages.Core
{
    public interface IInputParameter : IParameter
    {
        bool Required { get; }
    }
}
