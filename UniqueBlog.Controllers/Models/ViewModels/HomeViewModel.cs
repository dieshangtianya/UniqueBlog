using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Controllers.Models.ViewModels
{
    public class HomeViewModel : PageViewModelBase
    {
        public Dictionary<string, object> RouteValues { get; }

        public HomeViewModel()
        {
            RouteValues = new Dictionary<string, object>();
        }
    }
}
