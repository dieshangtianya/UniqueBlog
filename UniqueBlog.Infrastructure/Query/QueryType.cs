using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.Query
{
	/// <summary>
	/// 查询名称
	/// </summary>
	public enum QueryType
	{
		/// <summary>
		/// 动态生成SQL
		/// </summary>
		Dynamic = 0,
		/// <summary>
		/// 使用命名查询
		/// </summary>
		NamedQuery = 1
	}
}
