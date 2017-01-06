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
		private Lazy<IEnumerable<Category>> lazyCustomers;

		public BlogPostProxy(int id, Func<IEnumerable<Category>> requestCategoryFunc)
			: base(id)
		{
			lazyCustomers = new Lazy<IEnumerable<Entities.Category>>(requestCategoryFunc);
		}

		public override IEnumerable<Category> Categories
		{
			get
			{
				if (base.Categories == null)
				{
					base.Categories = this.lazyCustomers.Value;
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
