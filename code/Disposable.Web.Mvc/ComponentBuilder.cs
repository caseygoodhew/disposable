using System;

namespace Disposable.Web.Mvc
{
    public class ComponentBuilder : IComponentBuilder
    {
        public TComponent Build<TComponent, TComponentParameter>(TComponent component, Func<TComponentParameter, TComponentParameter> paramFunc)
            where TComponent : IComponent
            where TComponentParameter : ComponentParameter, new()
        {
            if (paramFunc == null)
            {
                return component;
            }

            return Decorate(component, paramFunc.Invoke(new TComponentParameter()));
        }

        private TComponent Decorate<TComponent>(TComponent component, BaseComponentParameter parameter) where TComponent : IComponent
        {
            if (parameter == null) { return component; }

            return Decorate(component, parameter.Decorate(component as Component).InnerParmeter);
        }
    }
}