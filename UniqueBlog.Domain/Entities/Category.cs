using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniqueBlog.Infrastructure;

namespace UniqueBlog.Domain.Entities
{
    /// <summary>
    /// Post category
    /// </summary>
    public class Category:EntityBase,IAggregateRoot
    {
        public Category(int id = default(int))
            : base(id)
        {

        }

		/// <summary>
		/// Category Name
		/// </summary>
		public string CategoryName { get; set; }

		/// <summary>
		/// Category Description
		/// </summary>
		public string CategoryDescription { get; set; }

		/// <summary>
		/// Created date of the category
		/// </summary>
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// Post amount the category contains
		/// </summary>
		public int PostAmount { get; set; }
    }
}
