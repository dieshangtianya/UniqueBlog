using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.Query
{
	/// <summary>
	/// 查询类
	/// </summary>
	public class Query
	{
		private IList<Criterion> criteria;

		#region Constructor

		public Query()
			: this(QueryType.Dynamic, new List<Criterion>())
		{

		}

		public Query(string queryName)
			: this(queryName, new List<Criterion>())
		{
			
		}

		public Query(string queryName, IList<Criterion> criteria)
			: this(QueryType.NamedQuery, criteria)
		{
			this.QueryName = queryName;
		}

		public Query(QueryType queryType, IList<Criterion> criteria)
		{
			this.QueryType = queryType;
			this.criteria = criteria;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// 查询名称
		/// </summary>
		public QueryType QueryType
		{
			get;
			private set;
		}

		/// <summary>
		/// 查询名称
		/// </summary>
		public string QueryName
		{
			get;
			set;
		}

		/// <summary>
		/// 查询条件列表
		/// </summary>
		public IEnumerable<Criterion> Criteria
		{
			get { return this.criteria; }
		}

		/// <summary>
		/// 查询操作符
		/// </summary>
		public QueryOperator QueryOperator { get; set; }

		/// <summary>
		/// 排序方式
		/// </summary>
		public OrderByClause OrderByClause { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// 判断当前Query是否为已命名的查询
		/// </summary>
		/// <returns></returns>
		public bool IsNamedQuery()
		{
			return this.QueryType != QueryType.Dynamic;
		}

		/// <summary>
		/// 添加查询条件
		/// </summary>
		/// <param name="criterion"></param>
		public void Add(Criterion criterion)
		{
			if (!IsNamedQuery())
			{
				this.criteria.Add(criterion);
			}
			else
			{
				throw new ApplicationException("You cannot add additional criteria to named query");
			}
		}

		#endregion
	}
}
