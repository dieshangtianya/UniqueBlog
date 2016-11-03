using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.Query
{
	/// <summary>
	/// 排序条件
	/// </summary>
	public class OrderByClause
	{
		/// <summary>
		/// 属性名称
		/// </summary>
		public string PropertyName { get; set; }

		/// <summary>
		/// 是否降序
		/// </summary>
		public bool Desc { get; set; }
	}
}
