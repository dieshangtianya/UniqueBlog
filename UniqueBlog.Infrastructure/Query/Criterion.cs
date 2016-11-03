using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.Query
{
	public class Criterion
	{
		#region Constructor

		public Criterion(string propertyName, object value, CriterionOperator criteriaOperator)
		{
			this.PropertyName = propertyName;
			this.Value = value;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// 条件的属性名称
		/// </summary>
		public string PropertyName
		{
			get;
			private set;
		}

		/// <summary>
		/// 条件的值
		/// </summary>
		public object Value
		{
			get;
			private set;
		}

		/// <summary>
		/// 条件的操作符
		/// </summary>
		public CriterionOperator CriterionOperator
		{
			get;
			private set;
		}

		#endregion

		#region Static methods

		public static Criterion Create<T>(Expression<Func<T, object>> expression, Object value, CriterionOperator criteriaOperator)
		{
			string propertyName = PropertyNameHelper.ResolvePropertyName<T>(expression);
			Criterion myCriterion = new Criterion(propertyName, value, criteriaOperator);
			return myCriterion;
		}

		#endregion
	}
}
