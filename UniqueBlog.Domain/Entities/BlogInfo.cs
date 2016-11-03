using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniqueBlog.Infrastructure ;

namespace UniqueBlog.Domain.Entities
{
    /// <summary>
    /// Blog信息
    /// </summary>
    public class Blog:IAggregate
    {
		public int BlogId { get; set; }

		public string BlogTitle { get; set; }

		public DateTime CreationDate { get; set; }

		public string Description { get; set; }
    }
}
