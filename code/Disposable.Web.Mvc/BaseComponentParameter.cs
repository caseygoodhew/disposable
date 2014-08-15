namespace Disposable.Web.Mvc
{
    public abstract class BaseComponentParameter
    {
        internal protected BaseComponentParameter InnerParmeter { get; set; }

        internal T Chain<T>() where T : BaseComponentParameter, new()
        {
            var result = new T { InnerParmeter = this };
            return result;
        }

        internal protected abstract BaseComponentParameter Decorate<T>(T obj) where T : Component;
    }
}