using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UniqueBlog.DBManager
{
	public class SQLServerDatabase : DbBase
	{
		public SQLServerDatabase(string connectionString)
			: base(connectionString)
		{

		}

		public override DbConnection CreateDbConnection()
		{
			return new SqlConnection(this.ConnectionString);
		}


		public override DbParameter CreateDbParameter(string parameterName, object value)
		{
			return new SqlParameter(this.ParameterPlaceholder + parameterName, value);
		}


		public new string ParameterPlaceholder
		{
			get { return "@"; }
		}
	}
}
