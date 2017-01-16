﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace UniqueBlog.DBManager
{
	/// <summary>
	/// Database基类
	/// </summary>
	public abstract class DbBase : IDatabase
	{
		private string connectionString;

		protected string ConnectionString {
			get { return connectionString; }
		}

		public DbBase(string connectionString)
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				throw new ArgumentException("Database connection string cannot be null or empty");
			}

			this.connectionString = connectionString;
		}

		public virtual DbConnection CreateDbConnection()
		{
			return null;
		}

		public virtual DbParameter CreateDbParameter()
		{
			return null;
		}

		public virtual DbParameter CreateDbParameter(string parameterName, object value)
		{
			return null;
		}

		public string ParameterPlaceholder
		{
			get
			{
				return "@";
			}
		}
	}
}
