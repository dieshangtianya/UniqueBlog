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
using UniqueBlog.Infrastructure;
using UniqueBlog.Infrastructure.Log;
using UniqueBlog.Infrastructure.MEF;
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
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._unitOfWork = unitOfWork;
            ((IUnitOfWorkRepository)this._postRepository).SetUnitOfWork(unitOfWork);
        }

        public PagedResult<PostDto> GetPostList(int blogId,int? categoryId, bool containDraft, int pageIndex, int pageSize)
        {
            PagedResult<PostDto> pagedResult = null;

            try
            {
                Query query = new Query();
                query.Add(new Criterion("BlogId", blogId, CriterionOperator.Equal));
                if(categoryId!=null && categoryId >0)
                {
                    query.Add(new Criterion("CategoryId", categoryId, CriterionOperator.Equal));
                }

                if (!containDraft)
                {
                    query.Add(new Criterion("Draft", false, CriterionOperator.Equal));
                }

                var pageData = _postRepository.FindBy(query, pageIndex, pageSize);

                IList<PostDto> postDtoList = new List<PostDto>();

                foreach (BlogPost post in pageData.Items)
                {
                    postDtoList.Add(post.ConvertTo(true));
                }

                pagedResult = new PagedResult<PostDto>(pageData.TotalRecordsCount, postDtoList);
            }
            catch (Exception exception)
            {
                logger.Error("There is an error happen", exception);
            }

            return pagedResult;
        }

        public PostDto GetPostById(int postId)
        {
            Query query = new Query();
            query.Add(new Criterion("BlogPostId", postId, CriterionOperator.Equal));

            var post = _postRepository.FindBy(query).FirstOrDefault();

            var postDto = post.ConvertTo(false);

            return postDto;
        }

        public int GetPostAmount(int blogId)
        {
            Query query = new Query();
            query.Add(new Criterion("BlogId", blogId, CriterionOperator.Equal));
            return _postRepository.GetPostAmount(query);
        }

        public bool PublishPost(PostDto postDto)
        {
            try
            {
                BlogPost post = postDto.ConvertTo();
                post.GenerateTimeStamps();

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

        public bool SavePost(PostDto postDto)
        {
            try
            {
                BlogPost post = postDto.ConvertTo();
                post.GenerateTimeStamps();
                this._postRepository.Save(post);
                this._unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                logger.Error("There is an error happen while saving a post", exception);
                return false;
            }
        }
    }
}