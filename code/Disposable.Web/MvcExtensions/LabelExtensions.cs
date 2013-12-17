using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Disposable.Common.Extensions;
using FluentValidation.Internal;

namespace Disposable.Web.MvcExtensions
{
    public static class LabelExtensions
    {
        public static MvcHtmlString FormattedLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            return FormattedLabelFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }
        
        public static MvcHtmlString FormattedLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            
            //string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var labelText = metadata.DisplayName;
            if (labelText.IsNullOrEmpty())
            {
                labelText = htmlFieldName.Split('.').Last().SplitPascalCase().ToTitleCase();
            }

            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            var tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            var span = new TagBuilder("span");
            span.SetInnerText(labelText);

            // assign <span> to <label> inner html
            tag.InnerHtml = span.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}