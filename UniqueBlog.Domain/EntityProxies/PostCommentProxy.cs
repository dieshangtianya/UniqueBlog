using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;

namespace UniqueBlog.Domain.EntityProxies
{
    public class PostCommentProxy : PostComment
    {
        private Lazy<BlogPost> lazyBlogPost;

        private bool hasLazyLoadingBlogPost = false;

        public PostCommentProxy(int commentId, Func<BlogPost> blogPostFunc)
            : base(commentId)
        {
            this.lazyBlogPost = new Lazy<Entities.BlogPost>(blogPostFunc);
        }

        public override BlogPost Post
        {
            get
            {
                if (base.Post == null && !hasLazyLoadingBlogPost)
                {
                    base.Post = this.lazyBlogPost.Value;
                    hasLazyLoadingBlogPost = true;
                }
                return base.Post;
            }
            set
            {
                base.Post = value;
            }
        }
    }
}
