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
    [Export(typeof(IBlogService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BlogService : IBlogService
    {
        [Import]
        public IBlogRepository blogRepository;

        private static readonly ILog logger = LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public BlogDto GetBlogByUserName()
        {
            var userName = "frwang";
            BlogDto blogDto = null;

            try
            {
                string procedureName = "sp_getblogbyusername";

                IList<Criterion> criteria = new List<Criterion>();
                criteria.Add(new Criterion("UserName", userName, CriterionOperator.Equal));
                Query query = new Query(procedureName, criteria);

                Blog blog = blogRepository.FindBy(query).FirstOrDefault();

                if (blog != null)
                {
                    blogDto = blog.ConvertTo();
                }
            }
            catch (Exception exception)
            {
                logger.Error("There is an error happen", exception);
            }
            return blogDto;
        }
    }
}
