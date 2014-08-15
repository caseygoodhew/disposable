using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Disposable.Web.Mvc;

namespace Disposable.Web2
{
    public static class ComponentBuilderExtensions
    {
        public static IComponent Field(this IComponentBuilder builder, Func<FieldParameter, FieldParameter> paramFunc = null)
        {
            return builder.ToBuilder().Build(new Field(), paramFunc);
        }

        public static IComponent TextBox(this IComponentBuilder builder, Func<TextBoxParameter, TextBoxParameter> paramFunc = null)
        {
            return builder.ToBuilder().Build(new TextBox(), paramFunc);
        }
    }
    
    public class Field : Component
    {
    }

    public class TextBox : Field
    {

    }
    
    public class FieldParameter : ComponentParameter
    {
    }

    public class TextBoxParameter : FieldParameter
    {
    }

    public static class FieldParameterExtensions
    {
        public static TComponentParameter Label<TComponentParameter>(this TComponentParameter obj) where TComponentParameter : FieldParameter, new()
        {
            return ComponentParameterFacade.Chain(obj, "label");
        }
    }

    public static class TextBoxParameterExtensions
    {
        public static TComponentParameter Rows<TComponentParameter>(this TComponentParameter obj) where TComponentParameter : TextBoxParameter, new()
        {
            return ComponentParameterFacade.Chain(obj, "rows");
        }
    }
}