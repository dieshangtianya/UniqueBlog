using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using UniqueBlog.Domain.Repository;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.DBManager;
using System.Data.Common;


namespace UniqueBlog.Repository
{
	[Export(typeof(IBlogRepository))]
	public class BlogRepository:IBlogRepository
	{
		private IDatabase _dbbase;

		public BlogRepository()
		{
			_dbbase = DatabaseFactory.CreateDataBase();
		}

		#region IBlogRepository members

		public IEnumerable<Blog> FindAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Blog> FindAll(int index, int count)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Blog> FindBy(Query query)
		{
			List<Blog> blogList = new List<Blog>();

			try
			{
				using (var dbConnection = _dbbase.CreateDbConnection())
				{
					dbConnection.Open();
					DbCommand command = dbConnection.CreateCommand();

					query.TranslateIntoSql(command);

					using (DbDataReader dataReader = command.ExecuteReader())
					{
						while (dataReader.Read())
						{
							Blog blog = this.GetBlogFromReader(dataReader);
							blogList.Add(blog);
						}
					}
				}

				return blogList;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}


		public Blog FindByUserName(string userName)
		{
			string procedureName="sp_getblogbyusername";

			IList<Criterion> criteria = new List<Criterion>();
			criteria.Add(new Criterion("UserName", userName, CriterionOperator.Equal));
			Query query = new Query(procedureName, criteria);

			IEnumerable<Blog> blogList = this.FindBy(query);
			return blogList.FirstOrDefault();
		}

		public IEnumerable<Blog> FindBy(Query query, int index, int count)
		{
			throw new NotImplementedException();
		}

		public Blog FindBy(int entityId)
		{
			throw new NotImplementedException();
		}

		public void Add(Blog entity)
		{
			throw new NotImplementedException();
		}

		public void Save(Blog entity)
		{
			throw new NotImplementedException();
		}

		public void Remove(Blog entity)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region private methods
		private Blog GetBlogFromReader(DbDataReader dataReader)
		{
			Blog blog = new Blog();
			blog.BlogId = (int)dataReader["BlogId"];
			blog.BlogTitle = dataReader["BlogTitle"].ToString();
			blog.Description = dataReader["Description"].ToString();
			blog.CreationDate = (DateTime)dataReader["CreationDate"];
			return blog;
		}
		#endregion
	}
}
