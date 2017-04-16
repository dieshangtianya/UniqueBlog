using System;
using System.Collections.Generic;
using System.Data;
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

		public override IDbConnection CreateDbConnection()
		{
			return new SqlConnection(this.ConnectionString);
		}

		public override IDbDataParameter CreateDbParameter()
		{
			return new SqlParameter();
		}

		public override IDbDataParameter CreateDbParameter(string parameterName, object value)
		{
			return new SqlParameter(this.ParameterPlaceholder + parameterName, value);
		}


		public new string ParameterPlaceholder
		{
			get { return "@"; }
		}
	}
}
