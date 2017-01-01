﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.DTO
{
	public class UserDto
	{
		public int UserId { get; set; }

		
		public string UserName { get; set; }

		public string NickName { get; set; }

		
		public string Password { get; set; }

		public string Email { get; set; }
	}
}
