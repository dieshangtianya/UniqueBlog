using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;
using UniqueBlog.DTO;

namespace UniqueBlog.Service.DtoMapper
{
	public static class CategoryMapper
	{
		public static CategoryDto ConvertTo(this Category category)
		{
			return DtoMapper.AutoMapperConfig.MapperInstance.Map<Category, CategoryDto>(category);
		}
	}
}
