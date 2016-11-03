using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniqueBlog.Controllers.ActionFilters;
using UniqueBlog.DTO;
using UniqueBlog.Service.Interfaces;

namespace UniqueBlog.Controllers
{
	[Export]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public class HomeController : Controller
	{
		public IBlogService BlogService { get; private set; }

		[ImportingConstructor]
		public HomeController(IBlogService service)
		{
			this.BlogService = service;
		}

		[CommonBlogDataFilter]
		public ActionResult Index()
		{ 
			return View();
		}
	}
}
