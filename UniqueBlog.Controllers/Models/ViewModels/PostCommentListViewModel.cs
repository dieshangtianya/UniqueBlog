using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.DTO;

namespace UniqueBlog.Controllers.Models.ViewModels
{
    public class PostCommentListViewModel:ViewModelBase
    {

        public IList<PostCommentDto> CommentList { get; set; }

        public PostCommentListViewModel()
        {

        }

        public PostCommentListViewModel(IEnumerable<PostCommentDto> commentList)
        {
            this.CommentList = new List<PostCommentDto>(commentList);
        }
    }
}
