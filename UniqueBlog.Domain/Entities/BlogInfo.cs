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
    public class Blog:EntityBase,IAggregateRoot
    {
        public Blog ()
            :base(0)
        {

        }

        public Blog (int id)
            :base(id)
        {

        }

		public string BlogTitle { get; set; }

		public DateTime CreationDate { get; set; }

		public string Description { get; set; }
    }
}
