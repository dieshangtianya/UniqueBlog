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
	public class PostService:IPostService
	{
		private IPostRepository _postRepository;

		[ImportingConstructor]
		public PostService(IPostRepository pository)
		{
			this._postRepository = pository;
		}

        public IEnumerable<PostDto> GetPostListByBlogId(int blogId)
		{
			Query query=new Query ();

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
            BlogPost post = postDto.ConvertTo();
            this._postRepository.Add(post);

            return true;
        }
    }
}
