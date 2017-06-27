using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using UniqueBlog.Domain.Repository;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Infrastructure.UnitOfWork;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.DBManager;
using UniqueBlog.Infrastructure;
using UniqueBlog.Domain.EntityProxies;

namespace UniqueBlog.Repository
{
    [Export(typeof(IPostRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PostRepository : IPostRepository, IUnitOfWorkRepository
    {
        private IDatabase _dbbase;
        private IUnitOfWork _unitOfWork;

        private string _baseSql = "select * from t_blog_post";

        private string _getCountBaseSql = "select count(*) from t_blog_post";

        #region constructor

        public PostRepository()
        {
            _dbbase = DatabaseFactory.CreateDataBase();
        }

        #endregion

        #region IPostRepository members

        public IEnumerable<BlogPost> FindAll()
        {
            List<BlogPost> postList = new List<BlogPost>();

            using (var dbConnection = _dbbase.CreateDbConnection())
            {
                dbConnection.Open();
                IDbCommand command = dbConnection.CreateCommand();
                command.CommandText = _baseSql;
                using (IDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        BlogPost post = this.GetBlogPostFromReader(dataReader);
                        postList.Add(post);
                    }
                }
            }

            return postList;
        }

        public PagedResult<BlogPost> FindAll(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogPost> FindBy(Query query)
        {
            List<BlogPost> postList = new List<BlogPost>();

            using (var dbConnection = _dbbase.CreateDbConnection())
            {
                dbConnection.Open();
                IDbCommand command = dbConnection.CreateCommand();
                query.TranslateIntoSql(command, _baseSql);
                using (IDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        BlogPost post = this.GetBlogPostFromReader(dataReader);
                        postList.Add(post);
                    }
                }
            }

            return postList;
        }

        public PagedResult<BlogPost> FindBy(Query query, int pageIndex, int pageSize)
        {
            PaginationQueryObject pageObject = PaginationQueryObjectFactory.CreateBlogPostPaginationQueryObject(query, pageIndex, pageSize);

            Query paginationQuery = QueryFactory.CreatePaginationQuery(pageObject);

            PagedResult<BlogPost> pagedResult = null;

            var postList = new List<BlogPost>();

            using (IDbConnection conn = this._dbbase.CreateDbConnection())
            {
                conn.Open();
                IDbCommand command = conn.CreateCommand();
                paginationQuery.TranslateIntoSql(command);

                IDataParameter parameterTotalRecords = this._dbbase.CreateDbParameter();
                parameterTotalRecords.ParameterName = "@TotalRecordAmount";
                parameterTotalRecords.Direction = ParameterDirection.Output;
                parameterTotalRecords.DbType = DbType.Int32;

                command.Parameters.Add(parameterTotalRecords);

                using (IDataReader dataReader = command.ExecuteReader())
                {

                    while (dataReader.Read())
                    {
                        BlogPost post = this.GetBlogPostFromReader(dataReader);
                        postList.Add(post);
                    }
                }

                var totalCount = (int)parameterTotalRecords.Value;

                pagedResult = new PagedResult<BlogPost>(totalCount, postList);
            }

            return pagedResult;
        }

        public void Add(BlogPost entity)
        {
            this._unitOfWork.RegisterNew(entity, this);
        }

        public void Save(BlogPost entity)
        {
            this._unitOfWork.RegisterAmended(entity, this);
        }

        public void Remove(BlogPost entity)
        {
            this._unitOfWork.RegisterRemoved(entity, this);
        }

        public int GetPostAmount(Query query)
        {
            int count = 0;

            using (var dbConnection = _dbbase.CreateDbConnection())
            {
                dbConnection.Open();
                IDbCommand command = dbConnection.CreateCommand();
                query.TranslateIntoSql(command, _getCountBaseSql);

                count = Convert.ToInt32(command.ExecuteScalar());
            }

            return count;
        }

        #endregion

        #region IUnitOfWorkRepository Implementation
        public void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void PersistCreationOf(IAggregateRoot entity)
        {
            var blogPost = entity as BlogPost;

            using (var dbConnection = this._dbbase.CreateDbConnection())
            {
                dbConnection.Open();

                IDbCommand dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = "sp_add_blogpost";
                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("BlogId", blogPost.BlogId));
                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("PostTitle", blogPost.Title));
                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("PostContent", blogPost.Content));
                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("PostPlainContent", blogPost.PlainContent));
                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("CreatedDate", blogPost.CreatedDate));
                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("LastUpdatedDate", blogPost.LastUpdatedDate));
                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("Tags", string.Join("|", blogPost.Tags)));

                int postId = Convert.ToInt32(dbCommand.ExecuteScalar());
                BlogPost.SetId(blogPost, postId);


                /********
                 * there are many ways to implement inserting multiple rows to table
                 * (1)multiple sql:
                 *      insert into table (column1,column2)values(v1,v2);
                 *      insert into table (column1,column2)values(v3,v4);
                 *      insert into table (column1,column2)values(v5,v6);
                 * (2)single sql: 
                 *      insert into table (column1,column2)values(v1,v2),(v3,v4),(v5,v6);
                 * (3)insert ...select
                 *      insert into table (column1,column2) select v1,v2 union all select v3,v4 union all select v5,v6
                 * (4)table value parameter
                 * (5)split a string to multiple row data.
                **********/


                IDbCommand relationCommand = dbConnection.CreateCommand();
                relationCommand.CommandText = "sp_add_blogpost_relation";
                relationCommand.CommandType = CommandType.StoredProcedure;
                /*SQL SERVER CALL************************************************************
                //DataTable dt = this.GetPostCategoryRelationship(blogPost);
                //relationCommand.Parameters.Add(this._dbbase.CreateDbParameter("postCategoryTable", dt));
                **************************************************************/

                /*MY SQL CALL****************************************************************/
                relationCommand.Parameters.Add(this._dbbase.CreateDbParameter("RelationData", this.GetPostCategoryIds(blogPost)));
                relationCommand.Parameters.Add(this._dbbase.CreateDbParameter("PostId",blogPost.Id));
                /**************************************************************/

                relationCommand.ExecuteNonQuery();
            }
        }

        public void PersistUpdateOf(IAggregateRoot entity)
        {
            var blogPost = entity as BlogPost;

            using (var dbConnection = this._dbbase.CreateDbConnection())
            {
                dbConnection.Open();

                IDbCommand dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = "sp_update_blogpost";
                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("PostId", blogPost.Id));
                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("PostTitle", blogPost.Title));
                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("PostContent", blogPost.Content));
                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("PostPlainContent", blogPost.PlainContent));
                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("Tags", string.Join("|", blogPost.Tags)));
                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("LastUpdatedDate", blogPost.LastUpdatedDate));

                DataTable dt = this.GetPostCategoryRelationship(blogPost);

                dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("PostCategoryTable", dt));

                dbCommand.ExecuteNonQuery();
            }
        }

        public void PersistDeleteOf(IAggregateRoot entity)
        {
        }

        #endregion

        #region private methods
        private BlogPost GetBlogPostFromReader(IDataReader dataReader)
        {
            int postId = (int)dataReader["BlogPostId"];

            BlogPost post = new BlogPostProxy(postId, () => LazyLoader.RequestCategory(postId), LazyLoader.RequestPostComments);
            post.Title = dataReader["PostTitle"].ToString();
            post.BlogId = (int)dataReader["BlogId"];
            post.Content = dataReader["PostContent"].ToString();
            post.PlainContent = dataReader["PostPlainContent"].ToString();
            post.CreatedDate = DateTime.Parse(dataReader["CreatedDate"].ToString());
            string tagStr = dataReader["Tags"].ToString();
            if (tagStr.Length > 0)
            {
                post.Tags = tagStr.Split(',');
            }

            return post;
        }

        private DataTable GetPostCategoryRelationship(BlogPost blogPost)
        {
            DataTable relationDt = new DataTable();
            relationDt.Columns.Add("PostId", typeof(int));
            relationDt.Columns.Add("CategoryId", typeof(int));

            foreach (var category in blogPost.Categories)
            {
                DataRow drow = relationDt.NewRow();
                drow["PostId"] = blogPost.Id;
                drow["CategoryId"] = category.Id;
                relationDt.Rows.Add(drow);
            }

            return relationDt;
        }

        private string GetPostCategoryIds(BlogPost blogPost)
        {
            StringBuilder sbuilder = new StringBuilder();
            foreach (var category in blogPost.Categories)
            {
                sbuilder.Append(category.Id);
                sbuilder.Append(",");
            }

            if (sbuilder.Length > 0)
            {
                sbuilder.Remove(sbuilder.Length - 1, 1);
            }

            return sbuilder.ToString();
        }

        #endregion
    }
}