using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.DTO;

namespace UniqueBlog.Service.Interfaces
{
    public interface IPostCommentService
    {
        bool PublishComment(PostCommentDto comment);

        bool DeleteComment(int commentId);

        bool ChangeCommentData(PostCommentDto comment);

        IEnumerable<PostCommentDto> GetPostCommentListByPostId(int postId);
    }
}
