using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Controllers.Cache;
using UniqueBlog.DTO;
using UniqueBlog.Infrastructure.MEF;
using UniqueBlog.Service;
using UniqueBlog.Service.Interfaces;

namespace UniqueBlog.Controllers
{
	public class CommonBlogData
	{

		private static readonly object lockObj = new object();

		#region single instance pattern implementation

		private static  CommonBlogData _currentInstance;

		public static CommonBlogData CurrentInstance
		{
			get
			{
				if (_currentInstance == null)
				{
					lock (lockObj)
					{
						if (_currentInstance==null)
						{
							_currentInstance = new CommonBlogData();
						}
					}
				}
				return _currentInstance;
			}
		}

		#endregion

		#region properties

		public BlogDto BlogInformation
		{
			get;
			private set;
		}

		public IList<CategoryDto> CategoryList
		{
			get;
			private set;
		}

		public int PostAmount
		{
			get;
			private set;
		}

        public IList<PostCommentDto> LatestComments
        {
            get;
            private set;
        }

        #endregion

        private IBlogService blogService;

		private ICategoryService categoryService;

        private IPostService postService;

        private IPostCommentService commentService;

		private WebCache webCache;

		public CommonBlogData()
		{
			//For creating instance manually from MEF, there are three approaches they are:
			//(1)Using the ExportFactory<T> of .net framework 4.5
			//(2)Using the reflection in .net to create the instance
			//(3)Using the MEF compositionContainer
			//Here we use the third way
			blogService = (IBlogService)MEFConfiguration.MEFContainer.GetExport<IBlogService>().Value;
			categoryService = (ICategoryService)MEFConfiguration.MEFContainer.GetExport<ICategoryService>().Value;
            postService = (IPostService)MEFConfiguration.MEFContainer.GetExport<IPostService>().Value;
            commentService = (IPostCommentService)MEFConfiguration.MEFContainer.GetExport<IPostCommentService>().Value;

			this.BlogInformation = blogService.GetBlogByUserName();
			this.CategoryList = categoryService.GetCategoriesByBlogId(this.BlogInformation.Id).ToList();
            this.PostAmount = postService.GetPostAmount(this.BlogInformation.Id);
            this.LatestComments = commentService.GetCommentList(this.BlogInformation.Id, 1, 6).Items.ToList();
		}

		public void RefreshCategoryList()
		{
			this.CategoryList = this.categoryService.GetCategoriesByBlogId(this.BlogInformation.Id).ToList();
		}

		public void RefreshBlogInformation()
		{
			this.BlogInformation = this.blogService.GetBlogByUserName();
		}

        public void RefreshPostAmount()
        {
            this.PostAmount = postService.GetPostAmount(this.BlogInformation.Id);
        }
	}
}
