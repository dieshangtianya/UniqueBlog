using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;
using UniqueBlog.DTO;

namespace UniqueBlog.Service.DtoMapper
{
	public static class PostMapper
	{
		public static PostDto ConvertTo(this BlogPost post)
		{
			return DtoMapper.AutoMapperConfig.MapperInstance.Map<BlogPost, PostDto>(post);
		}

        public static BlogPost ConvertTo(this PostDto postDto)
        {
            return DtoMapper.AutoMapperConfig.MapperInstance.Map<PostDto, BlogPost>(postDto);
        }
	}
}
