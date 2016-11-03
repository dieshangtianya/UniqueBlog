using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using UniqueBlog.DTO;
using UniqueBlog.Service;
using System.ComponentModel.Composition.Hosting;
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
					//For creating instance manually from MEF, there are three approaches they are:
					//(1)Using the ExportFactory<T> of .net framework 4.5
					//(2)Using the reflection in .net to create the instance
					//(3)Using the MEF compositionContainer
					//Here we use the third way
					IBlogService blogService = (IBlogService)MEF.MefConfiguration.MEFContainer.GetExport<IBlogService>().Value;
					ICategoryService categoryService = (ICategoryService)MEF.MefConfiguration.MEFContainer.GetExport<ICategoryService>().Value;
					_CurrentInstance = new CommonBlogData(blogService, categoryService);
				}
				return _CurrentInstance;
			}
		}

		#endregion


		#region properties

		public BlogDto BlogData
		{
			get;
			private set;
		}

		public IList<CategoryDto> CategoryList
		{
			get;
			private set;
		}

		#endregion

		public CommonBlogData(IBlogService blogService,ICategoryService categoryService)
		{
			BlogData = blogService.GetBlogByUserName();
			CategoryList = categoryService.GetCategoriesByBlogId(BlogData.BlogId).ToList();
		}
	}
}
