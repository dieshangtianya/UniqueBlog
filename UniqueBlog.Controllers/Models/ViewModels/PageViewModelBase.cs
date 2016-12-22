using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Controllers.Models.ViewModels
{
    /// <summary>
    /// Base viewmodel class of the blog page
    /// </summary>
    public class PageViewModelBase
    {
        public CommonBlogData GlobalBlogData { get; private set; }

        public PageViewModelBase()
        {
            this.GlobalBlogData = CommonBlogData.CurrentInstance;
        }
    }
}
