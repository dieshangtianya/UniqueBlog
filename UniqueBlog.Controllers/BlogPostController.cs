using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniqueBlog.Infrastructure.Log;
using UniqueBlog.Service.Interfaces;
using UniqueBlog.DTO;
using UniqueBlog.Controllers.Models;
using UniqueBlog.Controllers.Models.ViewModels;
using UniqueBlog.Controllers.ResponseResults;

namespace UniqueBlog.Controllers
{
	[Export]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public class BlogPostController:BlogControllerBase
	{
		private IPostService postService;
		private ICategoryService categoryService;
        private static readonly ILog logger =LogFactory.GetLog(typeof(BlogPostController));

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
            NewPostViewModel newPostVM = new NewPostViewModel();
            newPostVM.CategoryList = new List<SelectedItem>();

            var categoryList = this.categoryService.GetCategoriesByBlogId(newPostVM.GlobalBlogData.BlogData.BlogId);
            foreach(CategoryDto categoryItem in categoryList)
            {
                var selectedItem = new SelectedItem(categoryItem.CategoryId.ToString(), categoryItem.CategoryName);
                newPostVM.CategoryList.Add(selectedItem);
            }

			return View(newPostVM);
		}

        [HttpPost]
        public JsonResult SavePost(NewPostViewModel postViewModel)
        {
            PostDto postDto = new PostDto();
            postDto.BlogId = postViewModel.GlobalBlogData.BlogData.BlogId;
            postDto.Categories = new List<CategoryDto>();
            postViewModel.CategoryList.ForEach(x =>
            {
               var categoryItem = postViewModel.GlobalBlogData.CategoryList.First(c => c.CategoryId == Convert.ToInt32(x.ItemId));
                postDto.Categories.Add(categoryItem);
            });

            postDto.CreatedDate = DateTime.Now;
            postDto.Title = postViewModel.PostTitle;
            postDto.Tags = postViewModel.PostTags;
            postDto.Content = postViewModel.PostContent;

            bool flag = this.postService.AddPost(postDto);
            ResponseJsonResult responseJsonResult = new ResponseResults.ResponseJsonResult(flag);


            logger.Info("save a new post");

            if (!flag) {
                responseJsonResult.Message = "There is some error happen";
            }

            return Json(responseJsonResult);

        }

        [HttpPost]
        public ActionResult SaveDraft(PostDto post)
        {
            return RedirectToAction("Index", "Home");
        }
	}
}
