using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.DTO
{
	public class CategoryDto
	{
		/// <summary>
		/// Category Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Category Name
		/// </summary>
		public string CategoryName { get; set; }

		/// <summary>
		/// Category Description
		/// </summary>
		public string CategoryDescription { get; set; }

		/// <summary>
		/// Created Date of the cateogry
		/// </summary>
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// Amount of post the category contains
		/// </summary>
		public int PostAmount { get; set; }
	}
}
