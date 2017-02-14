using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.DTO;
using UniqueBlog.Infrastructure;

namespace UniqueBlog.Service.Interfaces
{
    public interface IPostCommentService
    {
        bool PublishComment(PostCommentDto comment);

        bool DeleteComment(int commentId);

        bool ChangeCommentData(PostCommentDto comment);

        IEnumerable<PostCommentDto> GetPostCommentListByPostId(int postId);

        PagedResult<PostCommentDto> GetCommentList(int blogId, int pageIndex, int pageSize);
    }
}
