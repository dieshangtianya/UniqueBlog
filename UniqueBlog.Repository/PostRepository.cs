using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using UniqueBlog.Domain.Repository;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.DBManager;



namespace UniqueBlog.Repository
{
	[Export(typeof(IPostRepository))]
	public class PostRepository : IPostRepository
	{
		private IDatabase _dbbase;

		private string _baseSql = "select * from t_blog_post";

		#region constructor

		public PostRepository()
		{
			_dbbase = DatabaseFactory.CreateDataBase();
		}

		#endregion

		#region IPostRepository members

		public IEnumerable<BlogPost> FindAll()
		{
			throw new NotImplementedException();
		}


		public IEnumerable<BlogPost> FindAll(int index, int count)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BlogPost> FindBy(Query query)
		{
			List<BlogPost> postList = new List<BlogPost>();

			try
			{
				using (var dbConnection = _dbbase.CreateDbConnection())
				{
					dbConnection.Open();
					DbCommand command = dbConnection.CreateCommand();
					query.TranslateIntoSql(command, _baseSql);
					using (DbDataReader dataReader = command.ExecuteReader())
					{
						while (dataReader.Read())
						{
							BlogPost post = this.GetBlogFromReader(dataReader);
							postList.Add(post);
						}
					}
				}

				return postList;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IEnumerable<BlogPost> FindBy(Query query, int index, int count)
		{
			throw new NotImplementedException();
		}

		public BlogPost FindBy(int entityId)
		{
			throw new NotImplementedException();
		}

		public void Add(BlogPost entity)
		{
			throw new NotImplementedException();
		}

		public void Save(BlogPost entity)
		{
			throw new NotImplementedException();
		}

		public void Remove(BlogPost entity)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region private methods
		private BlogPost GetBlogFromReader(DbDataReader dataReader)
		{
			BlogPost post = new BlogPost();
			post.PostId = (int)dataReader["BlogPostId"];
			post.Title = dataReader["PostTitle"].ToString();
			post.CategoryId = (int)dataReader["CategoryId"];
			post.BlogId = (int)dataReader["BlogId"];
			post.Content = dataReader["PostContent"].ToString();
			post.CreatedDate = DateTime.Parse(dataReader["CreatedDate"].ToString());
			string tagStr = dataReader["Tags"].ToString();
			if (tagStr.Length > 0)
			{
				post.Tags = tagStr.Split(',');
			}

			return post;
		}

		#endregion
	}
}
