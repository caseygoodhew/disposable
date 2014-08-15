using System;
using System.Web.Mvc;

namespace Disposable.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString Disposable(this HtmlHelper helper, Func<IComponentBuilder, IComponent> builderAction)
        {
            var builder = new ComponentBuilder();
            var component = builderAction.Invoke(builder) as Component;

            if (component == null)
            {
                throw new InvalidOperationException("component implements IComponent but is not derived from Component");
            }
            
            return new MvcHtmlString(component.GetType().Name + (component).Parameters);
        }
    }
}