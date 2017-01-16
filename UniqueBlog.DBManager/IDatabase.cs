using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.DBManager
{
	/// <summary>
	/// Database 接口，提供创建Database 对象的方法
	/// </summary>
	public interface IDatabase
	{
		DbConnection CreateDbConnection();

		DbParameter CreateDbParameter();

		DbParameter CreateDbParameter(string propertyName, object value);


		string ParameterPlaceholder { get; }
	}
}
