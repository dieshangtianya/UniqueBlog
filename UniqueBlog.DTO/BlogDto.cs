using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.DTO
{
	public class BlogDto
	{
		public int BlogId { get; set; }

		public string BlogTitle { get; set; }

		public DateTime CreationDate { get; set; }

		public string Description { get; set; }
	}
}
