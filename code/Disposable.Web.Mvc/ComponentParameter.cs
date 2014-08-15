namespace Disposable.Web.Mvc
{
    public class ComponentParameter : BaseComponentParameter
    {
        internal protected string Value;

        internal protected override BaseComponentParameter Decorate<T>(T obj)
        {
            obj.Parameters += " " + Value;
            return this;
        }

        internal protected T Chain<T>(string value) where T : ComponentParameter, new()
        {
            var result = Chain<T>();
            result.Value = value;
            return result;
        }
    }
}