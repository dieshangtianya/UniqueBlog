using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniqueBlog.Controllers.ActionFilters;
using UniqueBlog.Service.Interfaces;

namespace UniqueBlog.Controllers
{
	[Export]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public class BlogPostController:Controller
	{
		private IPostService postService;
		private ICategoryService categoryService;

		[ImportingConstructor]
		public BlogPostController(IPostService postService,ICategoryService categoryService)
		{
			this.postService = postService;
			this.categoryService = categoryService;
		}

		[CommonBlogDataFilter]
		public ActionResult PostList(int blogId)
		{
			ViewBag.PostList = this.postService.GetPostListByBlogId(blogId);
			return View();
		}

		[CommonBlogDataFilter]
		public ActionResult NewPost()
		{
			var viewBlogData =(CommonBlogData)ViewBag.CommonBlogData;
			ViewBag.CategoryList = this.categoryService.GetCategoriesByBlogId(viewBlogData.BlogData.BlogId);
			return View();
		}
	}
}
