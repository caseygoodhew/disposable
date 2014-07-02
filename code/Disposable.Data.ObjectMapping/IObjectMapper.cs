
namespace Disposable.Data.ObjectMapping
{
    public interface IObjectMapper<T>
    {
        void Something<T>() where T : new();
    }
}
