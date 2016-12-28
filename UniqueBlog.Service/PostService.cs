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
using UniqueBlog.Infrastructure.UnitOfWork;
using UniqueBlog.Service.DtoMapper;
using UniqueBlog.Service.Interfaces;

namespace UniqueBlog.Service
{
    [Export(typeof(IPostService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PostService : IPostService
    {
        private static readonly ILog logger = LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IPostRepository _postRepository;

        private IUnitOfWork _unitOfWork;

        [ImportingConstructor]
        public PostService(IPostRepository pository, IUnitOfWork unitOfWork)
        {
            this._postRepository = pository;
            this._unitOfWork = unitOfWork;

            ((IUnitOfWorkRepository)this._postRepository).SetUnitOfWork(unitOfWork);
        }

        public IEnumerable<PostDto> GetPostListByBlogId(int blogId)
        {
            IList<PostDto> postDtoList = new List<PostDto>();

            try
            {
                Query query = new Query();

                query.Add(new Criterion("BlogId", blogId, CriterionOperator.Equal));

                var postList = _postRepository.FindBy(query);

                foreach (BlogPost post in postList)
                {
                    postDtoList.Add(post.ConvertTo());
                }
            }
            catch (Exception exception)
            {
                logger.Error("There is an error happen", exception);
            }

            return postDtoList;
        }

        public PostDto GetPostById(int postId)
        {
            Query query = new Query();
            query.Add(new Criterion("BlogPostId", postId, CriterionOperator.Equal));

            var post = _postRepository.FindBy(postId);

            var postDto = post.ConvertTo();

            return postDto;
        }

        public bool AddPost(PostDto postDto)
        {
            try
            {
                BlogPost post = postDto.ConvertTo();
                this._postRepository.Add(post);
                this._unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                logger.Error("There is an error happen", exception);
                return false;
            }
        }
    }
}
