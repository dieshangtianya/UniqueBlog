using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.DBManager
{
	/// <summary>
	/// Database创建工厂
	/// </summary>
	public class DatabaseFactory
	{
		private static ConnectionStringSettings connectionSettings;

		private const string mySqlProvider = "MySql.Data.MySqlClient";
		private const string sqlServerProvider = "System.Data.SqlClient";

		/// <summary>
		/// 根据配置文件创建Database
		/// </summary>
		/// <returns></returns>
		public static IDatabase CreateDataBase()
		{
			IDatabase database;

			if (connectionSettings == null)
			{
				connectionSettings = ConfigurationManager.ConnectionStrings["UniqueBlogConnection"];
			}

			switch (connectionSettings.ProviderName)
			{
				case mySqlProvider:
					database = new MySQLDatabase(connectionSettings.ConnectionString);
					break;
				case sqlServerProvider:
					database = new SQLServerDatabase(connectionSettings.ConnectionString);
					break;
				default:
					throw new ArgumentException("The database provider currently is invalid");
			}

			return database;
		}
	}
}
