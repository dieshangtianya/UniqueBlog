using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniqueBlog.Service.Interfaces;
using UniqueBlog.DTO;

namespace UniqueBlog.Controllers
{
	[Export]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public class BlogPostController:BlogControllerBase
	{
		private IPostService postService;
		private ICategoryService categoryService;

		[ImportingConstructor]
		public BlogPostController(IPostService postService,ICategoryService categoryService)
		{
			this.postService = postService;
			this.categoryService = categoryService;
		}

		public ActionResult PostList(int blogId)
		{
			ViewBag.PostList = this.postService.GetPostListByBlogId(blogId);
			return View();
		}

		public ActionResult NewPost()
		{
			var viewBlogData =(CommonBlogData)ViewBag.CommonBlogData;
			ViewBag.CategoryList = this.categoryService.GetCategoriesByBlogId(viewBlogData.BlogData.BlogId);
			return View();
		}

        [HttpPost]
        public ActionResult SavePost(PostDto post)
        {
            return RedirectToAction("Index","Home");
        }
	}
}
