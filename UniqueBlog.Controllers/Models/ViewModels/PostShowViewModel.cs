using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.DTO;

namespace UniqueBlog.Controllers.Models.ViewModels
{
    public class PostShowViewModel:PageViewModelBase
    {
        public PostDto Post { get; set; }
    }
}
