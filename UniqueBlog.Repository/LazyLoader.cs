using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Repository;

namespace UniqueBlog.Repository
{
    public class LazyLoader
    {
        public static IEnumerable<Category> RequestCategory(int postId)
        {
            CategoryRepsitory categoryRepository = new CategoryRepsitory();
            Query query = new Query("sp_get_blogpost_categories");
            query.Add(new Criterion("PostId", postId, CriterionOperator.Equal));
            return categoryRepository.FindBy(query);
        }

        public static IEnumerable<PostComment> RequestPostComments(int postId)
        {
            PostCommentRepository commentRepository = new PostCommentRepository();
            Query query = new Query();
            query.Add(new Criterion("PostId", postId, CriterionOperator.Equal));

            return commentRepository.FindBy(query); 
        }

        public static PostComment RequestLinkComment(int postCommentId)
        {
            if (postCommentId == 0)
                return null;

            PostCommentRepository commentRepository = new PostCommentRepository();
            Query query = new Query();
            query.Add(new Criterion("CommentId", postCommentId, CriterionOperator.Equal));

            return commentRepository.FindBy(query).FirstOrDefault();
        }

        public static BlogPost RequestBlogPost(int postId)
        {
            PostRepository postRepository = new PostRepository();
            Query query = new Query();
            query.Add(new Criterion("BlogPostId", postId, CriterionOperator.Equal));

            return postRepository.FindBy(query).FirstOrDefault();
        }
    }
}
