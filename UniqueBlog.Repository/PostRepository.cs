﻿using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Data;
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

            using (var dbConnection = _dbbase.CreateDbConnection())
            {
                dbConnection.Open();
                DbCommand command = dbConnection.CreateCommand();
                query.TranslateIntoSql(command, _baseSql);
                using (DbDataReader dataReader = command.ExecuteReader())
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

        public IEnumerable<BlogPost> FindBy(Query query, int index, int count)
        {
            throw new NotImplementedException();
        }

        public BlogPost FindBy(int entityId)
        {
            BlogPost blogPost = null;

            using (var dbConnection = _dbbase.CreateDbConnection())
            {
                dbConnection.Open();
                DbCommand command= dbConnection.CreateCommand();
                string whereClause = " WHERE BlogPostId=@BlogPostId";
                command.CommandText = _baseSql + whereClause;
                command.Parameters.Add(_dbbase.CreateDbParameter("BlogPostId",entityId));

                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    while(dataReader.Read())
                    {
                        blogPost = this.GetBlogPostFromReader(dataReader);
                        blogPost.Categories = LazyLoader.RequestCategory(blogPost);
                        break;
                    }
                }
            }

            return blogPost;
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

        public void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region IUnitOfWorkRepository Implementation

        public void PersistCreationOf(IAggregateRoot entity)
        {
            var blogPost = entity as BlogPost;

            using (var dbConnection = this._dbbase.CreateDbConnection())
            {
                dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {

                    DbCommand dbCommand = dbConnection.CreateCommand();
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

                    DataTable dt = this.GetPostCategoryRelationship(blogPost);
                    DbCommand relationCommand = dbConnection.CreateCommand();
                    relationCommand.CommandText = "sp_add_blogpost_relation";
                    relationCommand.CommandType = CommandType.StoredProcedure;

                    relationCommand.Parameters.Add(this._dbbase.CreateDbParameter("postCategoryTable", dt));

                    relationCommand.ExecuteNonQuery();

                    transaction.Commit();

                }
            }
        }

        public void PersistUpdateOf(IAggregateRoot entity)
        {
            var blogPost = entity as BlogPost;

            using (var dbConnection = this._dbbase.CreateDbConnection())
            {
                dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    DbCommand dbCommand = dbConnection.CreateCommand();
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

                    transaction.Commit();

                }
            }
        }

        public void PersistDeleteOf(IAggregateRoot entity)
        {
        }

        #endregion

        #region private methods
        private BlogPost GetBlogPostFromReader(DbDataReader dataReader)
        {
            int postId = (int)dataReader["BlogPostId"];

            BlogPost post = new BlogPost(postId);
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

        #endregion
    }
}
