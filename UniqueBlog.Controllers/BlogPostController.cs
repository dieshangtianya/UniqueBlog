using System;
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
using UniqueBlog.Infrastructure;
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

        public ActionResult PostList(int blogId, int? category, int page)
        {
            PostListViewModel postListViewModel = new PostListViewModel();
            var pageSize = 5;

            var pagedResult = this.postService.GetPostList(blogId, category, page, pageSize);
            postListViewModel.PostList = pagedResult.Items;
            postListViewModel.PageNavigation = new Pagination(pagedResult.TotalRecordsCount, page, pageSize);
            postListViewModel.HasUserLogin = this.IsUserLogin();

            return View(postListViewModel);
        }

        [GlobalAuthorize]
        public ActionResult NewPost()
        {
            PostViewModel postViewModel = new PostViewModel();
            postViewModel.HasUserLogin = this.IsUserLogin();

            postViewModel.CategoryList = new List<SelectedItem>();
            var categoryList = this.categoryService.GetCategoriesByBlogId(postViewModel.GlobalBlogData.BlogInformation.Id);
            foreach (CategoryDto categoryItem in categoryList)
            {
                var selectedItem = new SelectedItem(categoryItem.Id.ToString(), categoryItem.CategoryName);
                postViewModel.CategoryList.Add(selectedItem);
            }
            var dd = HttpContext.Cache.Get("uu");
            return View(postViewModel);
        }

        [GlobalAuthorize]
        public ActionResult EditPost(int id)
        {
            var post = this.postService.GetPostById(id);

            PostViewModel newPostViewModel = new PostViewModel();
            newPostViewModel.CategoryList = this.GetCategoriesSelectedItem(newPostViewModel.GlobalBlogData.BlogInformation.Id);

            foreach (CategoryDto category in post.Categories)
            {
                var item = newPostViewModel.CategoryList.Where(t => t.ItemId == category.Id.ToString()).First();
                if (item != null)
                {
                    item.IsSelected = true;
                }
            }
            newPostViewModel.PostId = post.Id;
            newPostViewModel.HasUserLogin = this.IsUserLogin();
            newPostViewModel.PostContent = post.Content;
            newPostViewModel.PostTags = post.Tags;
            newPostViewModel.PostTitle = post.Title;
            newPostViewModel.PostViewType = Models.ViewModels.ViewType.Edit;

            return View("NewPost", newPostViewModel);
        }

        public ActionResult Post(int id)
        {
            PostDto postDto = this.postService.GetPostById(id);

            PostViewModel postViewModel = new PostViewModel();
            postViewModel.PostId = postDto.Id;
            postViewModel.PostTitle = postDto.Title;
            postViewModel.PostContent = postDto.Content;
            postViewModel.CreatedDate = postDto.CreatedDate;
            postViewModel.PostTags = postDto.Tags;

            return View(postViewModel);
        }

        [HttpPost]
        [GlobalAuthorize]
        public ActionResult SavePost(PostViewModel postViewModel)
        {
            if (postViewModel.CategoryList == null)
                return Json(null);

            var postDto = this.GeneratePostDtoFromViewModel(postViewModel);

            ResponseJsonResult responseJsonResult;

            if (postDto.Id == default(int))
            {
                responseJsonResult = this.SaveNewPost(postDto);
            }
            else
            {
                responseJsonResult = this.SavePostChange(postDto);
            }

            CommonBlogData.CurrentInstance.RefreshCategoryList();
            CommonBlogData.CurrentInstance.RefreshPostAmount();

            return Json(responseJsonResult);
        }

        [HttpPost]
        [GlobalAuthorize]
        public ActionResult SaveDraft(PostDto post)
        {
            return RedirectToAction("Index", "Home");
        }

        private ResponseJsonResult SaveNewPost(PostDto postDto)
        {
            var flag = false;

            postDto.CreatedDate = postDto.LastUpdatedDate = DateTime.Now;

            flag = this.postService.PublishPost(postDto);

            logger.Info("save a new post");

            ResponseJsonResult responseJsonResult = new ResponseResults.ResponseJsonResult(flag);

            if (!flag)
            {
                responseJsonResult.Message = "There is an error while publishing the post, please try again after a while";
            }

            return responseJsonResult;
        }

        private ResponseJsonResult SavePostChange(PostDto postDto)
        {
            var flag = false;

            postDto.LastUpdatedDate = DateTime.Now;

            flag = this.postService.SavePost(postDto);

            ResponseJsonResult responseJsonResult = new ResponseResults.ResponseJsonResult(flag);

            logger.Info("save changes of a post");

            if (!flag)
            {
                responseJsonResult.Message = "There is an error while saving the post, please try again after a while";
            }

            return responseJsonResult;
        }

        private PostDto GeneratePostDtoFromViewModel(PostViewModel postViewModel)
        {
            PostDto postDto = new PostDto();
            postDto.Id = postViewModel.PostId;
            postDto.BlogId = postViewModel.GlobalBlogData.BlogInformation.Id;
            postDto.Categories = new List<CategoryDto>();
            postViewModel.CategoryList.ForEach(x =>
            {
                var categoryItem = postViewModel.GlobalBlogData.CategoryList.First(c => c.Id == Convert.ToInt32(x.ItemId));
                postDto.Categories.Add(categoryItem);
            });

            postDto.Title = postViewModel.PostTitle;
            postDto.Tags = postViewModel.PostTags;
            postDto.Content = postViewModel.PostContent;
            postDto.PlainContent = postViewModel.PostPlainContent;

            return postDto;
        }

        #region Set category list for the NewPostViewModel
        private List<SelectedItem> GetCategoriesSelectedItem(int blogId)
        {
            var selectedItemList = new List<SelectedItem>();
            var categoryList = this.categoryService.GetCategoriesByBlogId(blogId);
            foreach (CategoryDto categoryItem in categoryList)
            {
                var selectedItem = new SelectedItem(categoryItem.Id.ToString(), categoryItem.CategoryName);
                selectedItemList.Add(selectedItem);
            }

            return selectedItemList;
        }

        #endregion
    }
}
