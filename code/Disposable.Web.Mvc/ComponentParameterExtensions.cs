namespace Disposable.Web.Mvc
{
    public static class ComponentParameterExtensions
    {
        public static TComponentParameter Class<TComponentParameter>(this TComponentParameter obj) where TComponentParameter : ComponentParameter, new()
        {
            return ComponentParameterFacade.Chain(obj, "class");
        }
    }
}
