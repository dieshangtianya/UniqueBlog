using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Controllers.Models.ViewModels
{
    public class PostViewModel:PageViewModelBase
    {
        public int PostId { get; set; }

        public string PostTitle { get; set; }

        public string PostContent { get; set; }

        public string PostPlainContent { get; set; }

        public string[] PostTags { get; set; }

        public List<SelectedItem> CategoryList { get; set; }

        public DateTime CreatedDate { get; set; }

        public ViewType PostViewType { get; set; }

        public bool IsDraft { get; set; }

        public PostViewModel()
        {
            this.PostId = 0;
            this.PostViewType = ViewType.New;
        }
    }
}
