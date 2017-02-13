using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.DTO
{
    public class PostCommentDto
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public PostDto Post { get; set; }

        public string CommentContent { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
