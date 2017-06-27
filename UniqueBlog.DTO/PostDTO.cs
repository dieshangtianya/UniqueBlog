using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.DTO
{
    public class PostDto
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string PlainContent { get; set; }

        public IList<CategoryDto> Categories { get; set; }

        public IList<PostCommentDto> Comments { get; set; }

        public int BlogId { get; set; }

        public bool Draft { get; set; }

        public string[] Tags { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
