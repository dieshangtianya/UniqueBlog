﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.DTO;

namespace UniqueBlog.Service.Interfaces
{
	public interface IBlogService
	{
		BlogDto GetBlogByUserName();
	}
}
