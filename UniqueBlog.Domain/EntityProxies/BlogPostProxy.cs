using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;

namespace UniqueBlog.Domain.EntityProxies
{
	public class BlogPostProxy : BlogPost
	{
		private Lazy<IEnumerable<Category>> lazyCategory;
        private Lazy<IEnumerable<PostComment>> lazyComment;

		public BlogPostProxy(int id, Func<IEnumerable<Category>> requestCategoryFunc,Func<IEnumerable<PostComment>> requestCommentFunc)
			: base(id)
		{
			lazyCategory = new Lazy<IEnumerable<Entities.Category>>(requestCategoryFunc);
            lazyComment = new Lazy<IEnumerable<PostComment>>(requestCommentFunc);
		}

		public override IEnumerable<Category> Categories
		{
			get
			{
				if (base.Categories == null)
				{
					base.Categories = this.lazyCategory.Value;
				}

				return base.Categories;
			}
			set
			{
				base.Categories = value;
			}
		}

        public override IEnumerable<PostComment> Comments
        {
            get
            {
                if (base.Comments == null)
                {
                    base.Comments = this.lazyComment.Value;
                }

                return base.Comments;
            }
        }
    }
}
