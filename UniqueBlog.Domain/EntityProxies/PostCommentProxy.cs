using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;

namespace UniqueBlog.Domain.EntityProxies
{
    public class PostCommentProxy:PostComment
    {
        private Lazy<BlogPost> lazyBlogPost;
        private Lazy<PostComment> lazyLinkComment;

        public PostCommentProxy(int commentId, Func<BlogPost> blogPostFunc,Func<PostComment> commentFunc)
            :base(commentId)
        {
            this.lazyBlogPost = new Lazy<Entities.BlogPost>(blogPostFunc);
            this.lazyLinkComment = new Lazy<PostComment>(commentFunc);
        }

        public override BlogPost Post
        {
            get
            {
                if(base.Post==null)
                {
                    base.Post = this.lazyBlogPost.Value;
                }
                return base.Post;
            }

            set
            {
                base.Post = value;
            }
        }

        public override PostComment LinkComment
        {
            get
            {
                if (base.LinkComment == null)
                {
                    base.LinkComment = this.lazyLinkComment.Value;
                }

                return base.LinkComment;
            }

            set
            {
                base.LinkComment = value;
            }
        }
    }
}
