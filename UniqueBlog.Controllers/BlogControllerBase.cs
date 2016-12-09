using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UniqueBlog.Controllers
{
    public class BlogControllerBase:Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            this.ViewBag.CommonBlogData = CommonBlogData.CurrentInstance;
        }
    }
}
