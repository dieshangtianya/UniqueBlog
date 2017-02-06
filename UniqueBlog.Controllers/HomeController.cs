using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniqueBlog.Controllers.Models.ViewModels;
using UniqueBlog.DTO;
using UniqueBlog.Service.Interfaces;

namespace UniqueBlog.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : BlogControllerBase
    {
        public IBlogService BlogService { get; private set; }

        [ImportingConstructor]
        public HomeController(IBlogService service)
        {
            this.BlogService = service;
        }

        public ActionResult Index(int? category, int? page = 1)
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            homeViewModel.RouteValues["page"] = page ?? 1;
            homeViewModel.RouteValues["pageUrl"] = Request.RawUrl;
            homeViewModel.RouteValues["category"] = category ?? 0;

            homeViewModel.HasUserLogin = this.IsUserLogin();
            return View(homeViewModel);
        }
    }
}
