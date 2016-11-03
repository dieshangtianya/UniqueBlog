using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using UniqueBlog.Service.Interfaces;
using UniqueBlog.Domain.Repository;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Domain.Entities;
using UniqueBlog.DTO;
using UniqueBlog.Service.DtoMapper;

namespace UniqueBlog.Service
{
	[Export(typeof(ICategoryService))]
	public class CategoryService:ICategoryService
	{
		private ICategoryRepository _CategoryRepository;

		[ImportingConstructor]
		public CategoryService(ICategoryRepository categoryRepository)
		{
			_CategoryRepository = categoryRepository;
		}

		public IEnumerable<DTO.CategoryDto> GetCategoriesByBlogId(int blogId)
		{
			Query query=new Query ();

			query.Add(new Criterion("BlogId", blogId, CriterionOperator.Equal));

			var categoryList = _CategoryRepository.FindBy(query);

			IList<CategoryDto> categoryDtoList = new List<CategoryDto>();

			foreach (Category category in categoryList)
			{
				categoryDtoList.Add(category.ConvertTo());
			}

			return categoryDtoList;
		}
	}
}
