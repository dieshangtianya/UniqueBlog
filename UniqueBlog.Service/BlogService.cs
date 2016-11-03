using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using UniqueBlog.Service.Interfaces;
using UniqueBlog.DTO;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Domain.Repository;
using UniqueBlog.Service.DtoMapper;


namespace UniqueBlog.Service
{
	[Export(typeof(IBlogService))]
	public class BlogService : IBlogService
	{
		[Import]
		public IBlogRepository blogRepository;

		public BlogDto GetBlogByUserName()
		{
			var userName = "frwang";
			Blog blog = blogRepository.FindByUserName(userName);
			BlogDto blogDto = null;

			if (blog != null)
			{
				blogDto = blog.ConvertTo();
			}

			return blogDto;

		}
	}
}
