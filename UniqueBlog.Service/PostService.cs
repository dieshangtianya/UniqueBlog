using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Repository;
using UniqueBlog.Domain.Entities;
using UniqueBlog.DTO;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Service.Interfaces;
using UniqueBlog.Service.DtoMapper;
using UniqueBlog.Infrastructure.UnitOfWork;

namespace UniqueBlog.Service
{
    [Export(typeof(IPostService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PostService : IPostService
    {
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
            Query query = new Query();

            query.Add(new Criterion("BlogId", blogId, CriterionOperator.Equal));

            var postList = _postRepository.FindBy(query);

            IList<PostDto> postDtoList = new List<PostDto>();

            foreach (BlogPost post in postList)
            {
                postDtoList.Add(post.ConvertTo());
            }

            return postDtoList;
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
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
