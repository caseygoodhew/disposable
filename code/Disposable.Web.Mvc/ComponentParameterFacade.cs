namespace Disposable.Web.Mvc
{
    public static class ComponentParameterFacade
    {
        public static TComponentParameter Chain<TComponentParameter>(TComponentParameter source, string value) where TComponentParameter : ComponentParameter, new()
        {
            return source.Chain<TComponentParameter>(value);
        }
    }
}