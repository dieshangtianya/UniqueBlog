using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.DBManager;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Domain.EntityProxies;
using UniqueBlog.Domain.Repository;
using UniqueBlog.Infrastructure;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Infrastructure.UnitOfWork;

namespace UniqueBlog.Repository
{
    public class PostCommentRepository : IPostCommentRepsoitory
    {
        private IDatabase _dbbase;

        private string _baseSql = "select * from t_comment";

        public PostCommentRepository()
        {
            _dbbase = DatabaseFactory.CreateDataBase();
        }

        #region IPostCommentRepository members implementation

        public void Add(PostComment entity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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

            int linkCommentId = dataReader["LinkCommentId"] == null ? 0 : (int)dataReader["LinkCommentId"];

            PostComment comment = new PostCommentProxy(commentId, () => LazyLoader.RequestBlogPost(postId), () => LazyLoader.RequestLinkComment(linkCommentId));

            comment.UserName = dataReader["UserName"].ToString();
            comment.CreatedDate = (DateTime)dataReader["CreatedDate"];
            comment.CommentContent = dataReader["CommentContent"].ToString();

            return comment;
        }
        #endregion
    }
}
