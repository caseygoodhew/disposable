namespace Disposable.Web.Mvc
{
    public class ComponentParameter : BaseComponentParameter
    {
        internal protected string Name;
        
        internal protected object Value;

        internal protected override BaseComponentParameter Decorate<T>(T obj)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                obj.Element.Attribute(Name, Value ?? string.Empty);
            }

            return this;
        }

        internal protected T Chain<T>(string name, object value) where T : ComponentParameter, new()
        {
            var result = Chain<T>();
            result.Name = name;
            result.Value = value;
            return result;
        }
    }
}