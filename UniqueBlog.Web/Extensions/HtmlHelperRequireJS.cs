using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace UniqueBlog.Web.Extensions
{
    public static class HtmlHelperRequireJS
    {
        public static MvcHtmlString RenderRequireJS(this HtmlHelper helper,string dataMainFile)
        {
            //src = "@Url.Content("~/ vendor / require / 2.1.15 / require.min.js")" data - main = "/app/post/new-post-main"
            string requirejsSrc = "../../vendor/require/2.1.15/require.min.js";
            var requireJSScript = new StringBuilder();
            var scriptFormat = "<script src='{0}' data-main='{1}'></script>";
            requireJSScript.Append(string.Format(scriptFormat, requirejsSrc, dataMainFile));
            return new MvcHtmlString(requireJSScript.ToString());
        }
    }
}