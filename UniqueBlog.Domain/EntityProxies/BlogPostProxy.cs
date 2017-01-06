using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;

namespace UniqueBlog.Domain.EntityProxies
{
    public class BlogPostProxy:BlogPost
    {
        private bool isCategoriesLoaded;

        private Func<BlogPost, IEnumerable<Category>> _requestCategoryFunc;

        public BlogPostProxy(Func<BlogPost, IEnumerable<Category>> requestCategoryFunc)
        {
            _requestCategoryFunc = requestCategoryFunc;
        }

        public override IEnumerable<Category> Categories
        {
            get
            {
                if (base.Categories == null)
                {
                    base.Categories = this._requestCategoryFunc(this);
                    this.isCategoriesLoaded = true;
                }

                return base.Categories;
            }
            set
            {
                base.Categories = value;
            }
        }
    }
}
