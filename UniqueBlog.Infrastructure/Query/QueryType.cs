using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.Query
{
	/// <summary>
	/// Query Type
	/// </summary>
	public enum QueryType
	{
		/// <summary>
		/// Dynamic sql
		/// </summary>
		Dynamic = 0,
		/// <summary>
		/// using named query
		/// </summary>
		NamedQuery = 1
	}
}
