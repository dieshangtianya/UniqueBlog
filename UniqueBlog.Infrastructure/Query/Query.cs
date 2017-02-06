using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.Query
{
	/// <summary>
	/// Query object
	/// </summary>
	public class Query:ICloneable
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
		/// Query type
		/// </summary>
		public QueryType QueryType
		{
			get;
			private set;
		}

		/// <summary>
		/// Query Name which means the stored procedure name
		/// </summary>
		public string QueryName
		{
			get;
			set;
		}

		/// <summary>
		/// Query criteria
		/// </summary>
		public IEnumerable<Criterion> Criteria
		{
			get { return this.criteria; }
		}

		/// <summary>
		/// Query operator
		/// </summary>
		public QueryOperator QueryOperator { get; set; }

		/// <summary>
		/// Order by clause
		/// </summary>
		public OrderByClause OrderByClause { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
        /// Determine whether current query is a named query
		/// </summary>
		/// <returns></returns>
		public bool IsNamedQuery()
		{
			return this.QueryType != QueryType.Dynamic;
		}

		/// <summary>
		/// Add criterion
		/// </summary>
		/// <param name="criterion"></param>
		public void Add(Criterion criterion)
		{
            this.criteria.Add(criterion);
        }

        public void Remove(Criterion criterion)
        {
            this.criteria.Remove(criterion);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
