﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniqueBlog.Controllers.Models;
using UniqueBlog.Controllers.Models.ViewModels;
using UniqueBlog.Controllers.ResponseResults;
using UniqueBlog.DTO;
using UniqueBlog.Infrastructure.Log;
using UniqueBlog.Service.Interfaces;

namespace UniqueBlog.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BlogPostController : BlogControllerBase
    {
        private IPostService postService;
        private ICategoryService categoryService;

        private static readonly ILog logger = LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public BlogPostController(IPostService postService, ICategoryService categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
        }

        public ActionResult PostList(int blogId)
        {
			PostListViewModel postListViewModel = new PostListViewModel();
			postListViewModel.PostList = this.postService.GetPostListByBlogId(blogId);
			postListViewModel.HasUserLogin = this.IsUserLogin();

            return View(postListViewModel);
        }

		[GlobalAuthorize]
        public ActionResult NewPost()
        {
            NewPostViewModel newPostVM = new NewPostViewModel();
			newPostVM.HasUserLogin = this.IsUserLogin();

            newPostVM.CategoryList = new List<SelectedItem>();
            var categoryList = this.categoryService.GetCategoriesByBlogId(newPostVM.GlobalBlogData.BlogData.BlogId);
            foreach (CategoryDto categoryItem in categoryList)
            {
                var selectedItem = new SelectedItem(categoryItem.CategoryId.ToString(), categoryItem.CategoryName);
                newPostVM.CategoryList.Add(selectedItem);
            }

            return View(newPostVM);
        }

		[GlobalAuthorize]
		public ActionResult EditPost(int id)
		{
			var post = this.postService.GetPostById(id);

			NewPostViewModel newPostViewModel = new NewPostViewModel();
			newPostViewModel.CategoryList = this.GetCategoriesSelectedItem(newPostViewModel.GlobalBlogData.BlogData.BlogId);

			foreach(CategoryDto category in post.Categories)
			{
				var item = newPostViewModel.CategoryList.Where(t => t.ItemId == category.CategoryId.ToString()).First();
				if (item != null) {
					item.IsSelected = true;
				}
			}
			newPostViewModel.HasUserLogin=this.IsUserLogin();
			newPostViewModel.PostContent=post.Content;
			newPostViewModel.PostTags=post.Tags;
			newPostViewModel.PostTitle=post.Title;
			return View("NewPost", newPostViewModel);
		}

        public ActionResult Post(int id)
        {
            PostDto postDto = this.postService.GetPostById(id);

            PostViewModel postViewModel = new PostViewModel();
            postViewModel.PostId = postDto.PostId;
            postViewModel.PostTitle = postDto.Title;
            postViewModel.PostContent = postDto.Content;
            postViewModel.CreatedDate = postDto.CreatedDate;
            postViewModel.PostTags = postDto.Tags;

            return View(postViewModel);
        }

        [HttpPost]
        public JsonResult SavePost(NewPostViewModel postViewModel)
        {
            if (postViewModel.CategoryList == null)
                return Json(null);

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
            postDto.PlainContent = postViewModel.PostPlainContent;

            bool flag = this.postService.AddPost(postDto);
            ResponseJsonResult responseJsonResult = new ResponseResults.ResponseJsonResult(flag);


            logger.Info("save a new post");

            if (!flag)
            {
                responseJsonResult.Message = "There is an error happen";
            }

            return Json(responseJsonResult);

        }

        [HttpPost]
        public ActionResult SaveDraft(PostDto post)
        {
            return RedirectToAction("Index", "Home");
		}

		#region Set category list for the NewPostViewModel
		private List<SelectedItem> GetCategoriesSelectedItem(int blogId)
		{
			var selectedItemList = new List<SelectedItem>();
			var categoryList = this.categoryService.GetCategoriesByBlogId(blogId);
			foreach (CategoryDto categoryItem in categoryList)
			{
				var selectedItem = new SelectedItem(categoryItem.CategoryId.ToString(), categoryItem.CategoryName);
				selectedItemList.Add(selectedItem);
			}

			return selectedItemList;
		}

		#endregion
	}
}
