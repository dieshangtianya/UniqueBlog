using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniqueBlog.Controllers.Models.ViewModels;

namespace UniqueBlog.Controllers
{
    public class BlogControllerBase: Controller
    {
		public BlogControllerBase()
		{

		}

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            return View();
        }

		public bool IsUserLogin()
		{
			if (HttpContext == null) {
				throw new NullReferenceException("HttpContext is null");
			}
			return HttpContext.Session[Constants.ConstantData.CurrentUserSessionKey] != null;
		}
    }
}
