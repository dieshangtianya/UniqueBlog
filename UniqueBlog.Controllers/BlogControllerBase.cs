using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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

        protected override void HandleUnknownAction(string actionName)
        {
            throw new HttpException(404, "Action is not found");
        }

        public ActionResult Login()
        {
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Logout()
        {
            this.Session[Constants.ConstantData.CurrentUserSessionKey] = null;
            return RedirectToAction("Index", "Home");
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
