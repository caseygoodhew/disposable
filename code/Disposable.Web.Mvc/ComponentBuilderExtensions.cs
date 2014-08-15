using System;

namespace Disposable.Web.Mvc
{
    public static class ComponentBuilderExtensions
    {
        public static ComponentBuilder ToBuilder(this IComponentBuilder builder)
        {
            var componentBuilder = builder as ComponentBuilder;

            if (componentBuilder == null)
            {
                throw new InvalidOperationException("builder implements IComponentBuilder but is not derived from ComponentBuilder");
            }

            return componentBuilder;
        }
            
        public static IComponent Component(this IComponentBuilder builder, Func<ComponentParameter, ComponentParameter> paramFunc = null)
        {
            return builder.ToBuilder().Build(new Component(), paramFunc);
        }
    }
}