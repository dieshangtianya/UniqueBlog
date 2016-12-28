using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Domain.Repository;
using UniqueBlog.DTO;
using UniqueBlog.Infrastructure.Log;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Service.DtoMapper;
using UniqueBlog.Service.Interfaces;

namespace UniqueBlog.Service
{
    [Export(typeof(ICategoryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _CategoryRepository;

        private static readonly ILog logger = LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _CategoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryDto> GetCategoriesByBlogId(int blogId)
        {
            IList<CategoryDto> categoryDtoList = new List<CategoryDto>();

            try
            {
                Query query = new Query();

                query.Add(new Criterion("BlogId", blogId, CriterionOperator.Equal));

                var categoryList = _CategoryRepository.FindBy(query);

                foreach (Category category in categoryList)
                {
                    categoryDtoList.Add(category.ConvertTo());
                }
            }
            catch (Exception exception)
            {
                logger.Error("There is an error happen", exception);
            }

            return categoryDtoList;
        }
    }
}
