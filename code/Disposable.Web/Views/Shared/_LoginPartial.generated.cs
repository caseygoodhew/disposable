﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Disposable.Web.Views.Shared
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/_LoginPartial.cshtml")]
    public partial class LoginPartial : System.Web.Mvc.WebViewPage<dynamic>
    {
        public LoginPartial()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Shared\_LoginPartial.cshtml"
 if (Request.IsAuthenticated) {

            
            #line default
            #line hidden
WriteLiteral("    ");

WriteLiteral("\r\n        Hello, ");

            
            #line 3 "..\..\Views\Shared\_LoginPartial.cshtml"
          Write(Html.ActionLink(User.Identity.Name, "Manage", "Account", routeValues: null, htmlAttributes: new { @class = "username", title = "Manage" }));

            
            #line default
            #line hidden
WriteLiteral("!\r\n");

            
            #line 4 "..\..\Views\Shared\_LoginPartial.cshtml"
        
            
            #line default
            #line hidden
            
            #line 4 "..\..\Views\Shared\_LoginPartial.cshtml"
         using (Html.BeginForm("LogOff", "Account", FormMethod.Post, new { id = "logoutForm" })) {
            
            
            #line default
            #line hidden
            
            #line 5 "..\..\Views\Shared\_LoginPartial.cshtml"
       Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
            
            #line 5 "..\..\Views\Shared\_LoginPartial.cshtml"
                                    

            
            #line default
            #line hidden
WriteLiteral("            <a");

WriteLiteral(" href=\"javascript:document.getElementById(\'logoutForm\').submit()\"");

WriteLiteral(">Log off</a>\r\n");

            
            #line 7 "..\..\Views\Shared\_LoginPartial.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    ");

WriteLiteral("\r\n");

            
            #line 9 "..\..\Views\Shared\_LoginPartial.cshtml"
} else {

            
            #line default
            #line hidden
WriteLiteral("    <ul>\r\n        <li>");

            
            #line 11 "..\..\Views\Shared\_LoginPartial.cshtml"
       Write(Html.ActionLink("Register", "Register", "Account", routeValues: null, htmlAttributes: new { id = "registerLink" }));

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n        <li>");

            
            #line 12 "..\..\Views\Shared\_LoginPartial.cshtml"
       Write(Html.ActionLink("Log in", "Login", "Account", routeValues: null, htmlAttributes: new { id = "loginLink" }));

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n    </ul>\r\n");

            
            #line 14 "..\..\Views\Shared\_LoginPartial.cshtml"
}

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
