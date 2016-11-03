using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.Query
{
	/// <summary>
	/// 条件操作符,用于条件的比较方式
	/// </summary>
	public enum CriterionOperator
	{
		Equal,
		Unequal,
		LessThan,
		LessThanOrEqual,
		GreaterThan,
		GreaterThanOrEqual,
		Like
	}
}
