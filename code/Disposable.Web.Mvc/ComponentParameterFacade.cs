namespace Disposable.Web.Mvc
{
    public static class ComponentParameterFacade
    {
        public static TComponentParameter Chain<TComponentParameter>(TComponentParameter source, string name, object value) where TComponentParameter : ComponentParameter, new()
        {
            return source.Chain<TComponentParameter>(name, value);
        }
    }
}