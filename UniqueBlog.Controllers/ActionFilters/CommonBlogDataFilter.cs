using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UniqueBlog.Controllers.ActionFilters
{
	public class CommonBlogDataFilter:ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			dynamic viewBag = filterContext.Controller.ViewBag;
			viewBag.CommonBlogData = CommonBlogData.CurrentInstance;
		}
	}
}
