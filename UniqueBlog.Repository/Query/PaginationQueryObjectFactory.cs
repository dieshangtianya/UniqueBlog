using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Infrastructure.Query;

namespace UniqueBlog.Repository
{
    public class PaginationQueryObjectFactory
    {
        public static PaginationQueryObject CreateBlogPostPaginationQueryObject(Query parameterQuery, int pageIndex, int pageSize)
        {
            Query tempQuery = (Query)parameterQuery.Clone();

            var categoryCriterion = tempQuery.Criteria.FirstOrDefault(c => c.PropertyName == "CategoryId");
            if (categoryCriterion != null)
            {
                tempQuery.Remove(categoryCriterion);
            }

            string sqlWhere = tempQuery.TranslateIntoWhereSql();

            if (categoryCriterion != null)
            {
                sqlWhere += string.Format(" AND EXISTS(SELECT * FROM t_post_category WHERE PostId=BlogPostId and CategoryId={0})", categoryCriterion.Value);
            }

            PaginationQueryObject pageObject = new PaginationQueryObject("t_blog_post");
            pageObject.Fields = " BlogPostID, BlogId, PostTitle, PostContent, PostPlainContent, CreatedDate, LastUpdatedDate, Tags ";
            pageObject.SqlWhere = sqlWhere;
            pageObject.GroupFileds = "";
            pageObject.OrderByFields = "ORDER BY BlogPostId DESC";
            pageObject.PageIndex = pageIndex;
            pageObject.PageSize = pageSize;

            return pageObject;
        }

        public static PaginationQueryObject CreateCommentPaginationQueryObject(Query parameterQuery,int pageIndex,int pageSize)
        {
            var sqlWhere = parameterQuery.TranslateIntoWhereSql();

            PaginationQueryObject pageObject = new PaginationQueryObject("t_comment");
            pageObject.Fields = " CommentId,BlogId,PostId,UserId,UserName,CommentContent,CreatedDate ";
            pageObject.SqlWhere = sqlWhere;
            pageObject.GroupFileds = "";
            pageObject.OrderByFields = "ORDER BY CreatedDate DESC";
            pageObject.PageIndex = pageIndex;
            pageObject.PageSize = pageSize;

            return pageObject;
        }
    }
}
