using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.DTO;

namespace UniqueBlog.Controllers.Models.ViewModels
{
    public class NewPostViewModel:PageViewModelBase
    {
        public string PostTitle { get; set; }

        public string PostContent { get; set; }

        public string[] PostTags { get; set; }

        public List<SelectedItem> CategoryList { get; set; }
    }
}
