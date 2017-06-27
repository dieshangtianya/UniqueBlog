using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.DTO;
using UniqueBlog.Infrastructure;

namespace UniqueBlog.Service.Interfaces
{
    public interface IPostService
    {
        PagedResult<PostDto> GetPostList(int blogId, int? categoryId, bool containDraft, int pageIndex, int pageSize);

        PostDto GetPostById(int postId);

        int GetPostAmount(int blogId);

        bool PublishPost(PostDto post);

        bool SavePost(PostDto post);
    }
}
