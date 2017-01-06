using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniqueBlog.Infrastructure;

namespace UniqueBlog.Domain.Entities
{
	/// <summary>
	/// User basic information
	/// </summary>
	public class User : EntityBase,IAggregateRoot
    {
        public User (int id=default(int))
            :base(id)
        {

        }
		/// <summary>
		/// 用户名称
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Email地址
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// 昵称
		/// </summary>
		public string NickName { get; set; }

		/// <summary>
		/// 登录密码
		/// </summary>
		public string Password { get; set; }
	}
}
