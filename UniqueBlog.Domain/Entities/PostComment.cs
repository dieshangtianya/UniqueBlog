using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Infrastructure;

namespace UniqueBlog.Domain.Entities
{
    public class PostComment:EntityBase, IAggregateRoot
    {
        public string UserName { get; set; }

        public virtual BlogPost Post { get;  set; }

        public virtual PostComment LinkComment { get; set; }

        public string CommentContent { get; set; }

        public DateTime CreatedDate { get; set; }

        #region Constructor

        public PostComment(int id = default(int))
            : base(id)
        {

        }

        #endregion
    }
}
