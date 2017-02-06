using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Controllers.Models.ViewModels
{
    public class HomeViewModel:PageViewModelBase
    {
        public int Page { get; set; }

        public int? Category { get; set; }
    }
}
