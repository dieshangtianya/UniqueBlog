using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Infrastructure;

namespace UniqueBlog.Domain.Entities
{
    /// <summary>
    /// Blog post information which represent a aggregate root
    /// </summary>
    public class BlogPost : EntityBase, IAggregateRoot
    {
        #region Private fields

        private IEnumerable<Category> _categories;

        #endregion

        #region Public business data properties

        /// <summary>
        /// Get or set the post title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Get or set the post html content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Get or set the plain content of the post(without html tags)
        /// </summary>
        public string PlainContent { get; set; }

        /// <summary>
        /// Get or set the categories of the post
        /// </summary>
        public virtual IEnumerable<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                if (_categories != null)
                {
                    this.CheckCategoriesChanged(value);
                }
                _categories = value;
            }
        }

        public virtual IEnumerable<PostComment> Comments { get; set; }

        /// <summary>
        /// Get or set the blog id the post belong to
        /// </summary>
        public int BlogId { get; set; }

        /// <summary>
        /// Get or set the post tags
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// Get or set the created date of the post
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Get or set the last updated date of the post
        /// </summary>
        public DateTime LastUpdatedDate { get; set; }

        #endregion

        #region Business Logical Properties

        /// <summary>
        /// Whether the categories of the post has been changed
        /// </summary>
        public bool IsCategoriesChanged
        {
            get;
            private set;
        }
        #endregion

        #region Constructor

        public BlogPost(int id = default(int))
            : base(id)
        {
            this.IsCategoriesChanged = false;
        }

        #endregion

        #region Business Operations
        public void GenerateTimeStamps()
        {
            if (this.Id == default(int))
            {
                this.CreatedDate = this.LastUpdatedDate = DateTime.Now;
            }
            else
            {
                this.LastUpdatedDate = DateTime.Now;
            }
        }

        private void CheckCategoriesChanged(IEnumerable<Category> tempCategories)
        {
            if (this.Categories != null)
            {
                var distinct = this.Categories.Except(tempCategories, new EntityEqualityCompare<Category>((x, y) => x.Id == y.Id));
                if (distinct.Count() > 0)
                {
                    this.IsCategoriesChanged = true;
                }
            }
        }
        #endregion
    }
}
