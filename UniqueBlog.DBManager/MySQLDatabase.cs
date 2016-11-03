using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UniqueBlog.DBManager
{
	public class MySQLDatabase : DbBase
	{

		public MySQLDatabase(string connectionString)
			: base(connectionString)
		{

		}

		public override DbConnection CreateDbConnection()
		{
			return new MySqlConnection(this.ConnectionString);
		}

		public override DbParameter CreateDbParameter(string parameterName, object value)
		{
			return new MySqlParameter(this.ParameterPlaceholder + parameterName, value);
		}


		public new string ParameterPlaceholder
		{
			get { return "@"; }
		}
	}
}
