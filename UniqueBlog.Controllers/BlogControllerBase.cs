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
        public CommonBlogData GlobalBlogData { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            this.GlobalBlogData = CommonBlogData.CurrentInstance;
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            return View();
        }
    }
}
