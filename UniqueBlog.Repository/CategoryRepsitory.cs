using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using UniqueBlog.DBManager;
using UniqueBlog.Domain.Repository;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Infrastructure;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Infrastructure.UnitOfWork;
using System.Data.Common;

namespace UniqueBlog.Repository
{
	[Export(typeof(ICategoryRepository))]
	public class CategoryRepsitory:ICategoryRepository
	{
		private IDatabase _dbbase;

		private string _baseSql = "SELECT * FROM t_category";

		[ImportingConstructor]
		public CategoryRepsitory()
		{
			this._dbbase = DatabaseFactory.CreateDataBase();
		}

		#region ICategoryRepository members

		public IEnumerable<Category> FindAll()
		{
			return new List<Category>();
		}

		public IEnumerable<Category> FindAll(int index, int count)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Category> FindBy(Query query)
		{
			List<Category> categoryList = new List<Category>();

			try
			{
				using (var connection = _dbbase.CreateDbConnection())
				{
					connection.Open();
					DbCommand command = connection.CreateCommand();
					query.TranslateIntoSql(command, _baseSql);

					using (DbDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Category postCategory = this.GetCategoryFromReader(reader);
							categoryList.Add(postCategory);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return categoryList;
		}

		public IEnumerable<Category> FindBy(Query query, int index, int count)
		{
			throw new NotImplementedException();
		}

		public Category FindBy(int entityId)
		{
			throw new NotImplementedException();
		}

		public void Add(Category entity)
		{
			throw new NotImplementedException();
		}

		public void Save(Category entity)
		{
			throw new NotImplementedException();
		}

		public void Remove(Category entity)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region private methods

		private Category GetCategoryFromReader(DbDataReader reader)
		{
			Category category = new Category();
			category.CategoryId = (int)reader["CategoryId"];
			category.CategoryName = reader["CategoryName"].ToString();
			category.CategoryDescription = reader["Description"].ToString();
			category.CreatedDate = (DateTime)reader["CreatedDate"];
			return category;
		}

		#endregion
	}
}
