using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniqueBlog.Infrastructure;

namespace UniqueBlog.Domain.Entities
{
    /// <summary>
    /// 博文类型
    /// </summary>
    public class Category:EntityBase,IAggregateRoot
    {
        public Category(int id = default(int))
            : base(id)
        {

        }

		/// <summary>
		/// 类别名称
		/// </summary>
		public string CategoryName { get; set; }

		/// <summary>
		/// 类别描述
		/// </summary>
		public string CategoryDescription { get; set; }

		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime CreatedDate { get; set; }
    }
}
