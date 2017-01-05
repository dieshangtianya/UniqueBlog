using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Repository;

namespace UniqueBlog.Repository
{
    public class LazyLoader
    {
        public static IEnumerable<Category> RequestCategory(BlogPost post)
        {
            CategoryRepsitory categoryRepository = new CategoryRepsitory();
            Query query = new Query("sp_get_blogpost_categories");
            query.Add(new Criterion("PostId", post.Id, CriterionOperator.Equal));
            return categoryRepository.FindBy(query);
        }
    }
}
