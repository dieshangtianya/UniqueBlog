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
		#region single instance pattern implementation

		private static  CommonBlogData _CurrentInstance;

		public static CommonBlogData CurrentInstance
		{
			get
			{
				if (_CurrentInstance == null)
				{
					_CurrentInstance = new CommonBlogData();
				}
				return _CurrentInstance;
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

		#endregion

		private IBlogService blogService;

		private ICategoryService categoryService;

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

			webCache = new WebCache();

			this.BlogInformation = blogService.GetBlogByUserName();
			this.CategoryList = categoryService.GetCategoriesByBlogId(this.BlogInformation.Id).ToList();
		}

		public void RefreshCategoryList()
		{
			this.CategoryList = this.categoryService.GetCategoriesByBlogId(this.BlogInformation.Id).ToList();
		}

		public void RefreshBlogData()
		{
			this.BlogInformation = this.blogService.GetBlogByUserName();
		}
	}
}
