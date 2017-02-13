using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using UniqueBlog.DBManager;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Domain.EntityProxies;
using UniqueBlog.Domain.Repository;
using UniqueBlog.Infrastructure;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Infrastructure.UnitOfWork;

namespace UniqueBlog.Repository
{
    [Export(typeof(IPostCommentRepsoitory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PostCommentRepository : IPostCommentRepsoitory
    {
        private IDatabase _dbbase;

        private string _baseSql = "select * from t_comment";

        private string _baseDeleteSql = "delete from t_comment";

        public PostCommentRepository()
        {
            _dbbase = DatabaseFactory.CreateDataBase();
        }

        #region IPostCommentRepository members implementation

        public void Add(PostComment entity)
        {
            using (DbConnection dbConnection = _dbbase.CreateDbConnection())
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    DbCommand dbCommand = dbConnection.CreateCommand();
                    dbCommand.CommandText = "sp_add_comment";
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Transaction = transaction;

                    dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("BlogId", entity.BlogId));
                    dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("PostId", entity.Post.Id));
                    dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("UserName", entity.UserName));
                    dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("UserId", entity.UserId == 0 ? DBNull.Value : (object)entity.UserId));
                    dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("CommentContent", entity.CommentContent));
                    dbCommand.Parameters.Add(this._dbbase.CreateDbParameter("CreatedDate", entity.CreatedDate));

                    dbCommand.ExecuteScalar();

                    transaction.Commit();
                }
            }
        }

        public IEnumerable<PostComment> FindAll()
        {
            List<PostComment> commentList = new List<PostComment>();

            using (DbConnection connection = _dbbase.CreateDbConnection())
            {
                DbCommand command = connection.CreateCommand();
                command.CommandText = _baseSql;
                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        PostComment comment = this.GetPostCommentFromReader(dataReader);
                        commentList.Add(comment);
                    }
                }
            }

            return commentList;
        }

        public PagedResult<PostComment> FindAll(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostComment> FindBy(Query query)
        {
            List<PostComment> commentList = new List<PostComment>();

            using (var dbConnection = _dbbase.CreateDbConnection())
            {
                dbConnection.Open();
                DbCommand command = dbConnection.CreateCommand();
                query.TranslateIntoSql(command, _baseSql);
                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        PostComment comment = this.GetPostCommentFromReader(dataReader);
                        commentList.Add(comment);
                    }
                }
            }

            return commentList;
        }

        public PagedResult<PostComment> FindBy(Query query, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void Remove(PostComment entity)
        {
            Query query = new Query();
            query.Add(new Criterion("CommentId", entity.Id, CriterionOperator.Equal));
            using (var dbConnection = _dbbase.CreateDbConnection())
            {
                dbConnection.Open();
                DbCommand command = dbConnection.CreateCommand();
                query.TranslateIntoSql(command, _baseDeleteSql);

                command.ExecuteNonQuery();
            }
        }

        public void Save(PostComment entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region private methods
        private PostComment GetPostCommentFromReader(DbDataReader dataReader)
        {
            int commentId = (int)dataReader["CommentId"];
            int postId = (int)dataReader["PostId"];

            PostComment comment = new PostCommentProxy(commentId, () => LazyLoader.RequestBlogPost(postId));

            comment.UserName = dataReader["UserName"].ToString();
            comment.CreatedDate = (DateTime)dataReader["CreatedDate"];
            comment.CommentContent = dataReader["CommentContent"].ToString();
            comment.UserId = dataReader["UserId"].ToString() == "" ? 0 : (int)dataReader["UserId"];

            return comment;
        }
        #endregion
    }
}
