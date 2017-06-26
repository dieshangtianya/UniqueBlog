using System;
using System.Collections.Generic;
using System.Data;
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


namespace UniqueBlog.Repository
{
    [Export(typeof(ICategoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CategoryRepsitory : ICategoryRepository
    {
        private IDatabase _dbbase;

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

        public PagedResult<Category> FindAll(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> FindBy(Query query)
        {
            List<Category> categoryList = new List<Category>();

            using (var connection = _dbbase.CreateDbConnection())
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                query.TranslateIntoSql(command);

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Category postCategory = this.GetCategoryFromReader(reader);
                        categoryList.Add(postCategory);
                    }
                }
            }

            return categoryList;
        }

        public PagedResult<Category> FindBy(Query query, int pageIndex, int pageSize)
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

        private Category GetCategoryFromReader(IDataReader reader)
        {
            int categoryId = (int)reader["CategoryId"];

            Category category = new Category(categoryId);
            category.CategoryName = reader["CategoryName"].ToString();
            category.CategoryDescription = reader["Description"].ToString();
            category.CreatedDate = (DateTime)reader["CreatedDate"];
            category.PostAmount = Convert.ToInt32(reader["PostAmount"].ToString());
            return category;
        }

        #endregion
    }
}
