using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Disposable.Web.Common
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// DisplayForList is a replacement for <see cref="System.Web.Mvc.HtmlHelper.DisplayFor"/> to work around defects in the core mvc component.
        /// </summary>
        /// <typeparam name="TModel">The view model.</typeparam>
        /// <typeparam name="TValue">The value to render.</typeparam>
        /// <param name="html">System.Web.Mvc.HtmlHelper</param>
        /// <param name="expression">The function to the values to render.</param>
        /// <returns>An McvHtmlString</returns>
        public static MvcHtmlString DisplayForList<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Func<TModel, IEnumerable<TValue>> expression)
        {
            return html.DisplayForList(expression, null);
        }

        /// <summary>
        /// DisplayForList is a replacement for <see cref="System.Web.Mvc.HtmlHelper.DisplayFor"/> to work around defects in the core mvc component.
        /// </summary>
        /// <typeparam name="TModel">The view model.</typeparam>
        /// <typeparam name="TValue">The value to render.</typeparam>
        /// <param name="html">System.Web.Mvc.HtmlHelper</param>
        /// <param name="expression">The function to the values to render.</param>
        /// <param name="templateName">The template name to use.</param>
        /// <returns>An McvHtmlString</returns>
        public static MvcHtmlString DisplayForList<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Func<TModel, IEnumerable<TValue>> expression,
            string templateName)
        {
            var tempResult = new StringBuilder();

            var values = expression.Invoke(html.ViewData.Model);

            foreach (var item in values)
            {
                tempResult.Append(html.DisplayFor(m => item, templateName));
            }

            return MvcHtmlString.Create(tempResult.ToString());
        }
    }
}
