namespace Disposable.Web.Mvc
{
    public static class ComponentParameterExtensions
    {
        public static TComponentParameter Class<TComponentParameter>(this TComponentParameter obj, string value) where TComponentParameter : ComponentParameter, new()
        {
            return ComponentParameterFacade.Chain(obj, "class", value);
        }
    }
}
