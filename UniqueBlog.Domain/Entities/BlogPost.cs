using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Infrastructure;

namespace UniqueBlog.Domain.Entities
{
	public class BlogPost : IAggregate
	{
		public int PostId { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

        public string PlainContent { get; set; }

		public IList<Category> Categories { get; set; }

		public int BlogId { get; set; }

		public string[] Tags { get; set; }

		public DateTime CreatedDate { get; set; }

	}
}
