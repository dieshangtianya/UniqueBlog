using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;
using UniqueBlog.DTO;

namespace UniqueBlog.Service.DtoMapper
{
	public static class BlogMapper
	{
		public static BlogDto ConvertTo(this Blog blog)
		{
			return DtoMapper.AutoMapperConfig.MapperInstance.Map<Blog,BlogDto>(blog);
		}
	}
}
