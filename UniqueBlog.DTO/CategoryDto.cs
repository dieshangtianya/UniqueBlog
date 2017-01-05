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
		/// 类别编号
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// 类别名称
		/// </summary>
		public string CategoryName { get; set; }

		/// <summary>
		/// 类别描述
		/// </summary>
		public string CategoryDescription { get; set; }

		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime CreatedDate { get; set; }
	}
}
