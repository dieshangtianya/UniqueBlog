using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.DTO;

namespace UniqueBlog.Service.Interfaces
{
	public interface IPostService
	{
		IEnumerable<PostDto> GetPostListByBlogId(int blogId);

        PostDto GetPostById(int postId);

        bool AddPost(PostDto post);
	}
}
