using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Domain.EntityProxies;
using UniqueBlog.DTO;
using AutoMapper;


namespace UniqueBlog.Service.DtoMapper
{
	public static class AutoMapperConfig
	{
		public static IMapper MapperInstance { get; set; }

		public static void InitializeTypeMapper()
		{
			var mappingConfigInstance = new MapperConfiguration(cfg =>
			{
				//User ---- UserDto
				cfg.CreateMap<UserDto, User>();
				cfg.CreateMap<User, UserDto>();

				//Blog ---- BlogDto
				cfg.CreateMap<BlogDto, Blog>();
				cfg.CreateMap<Blog, BlogDto>();

				//Category ---- CategoryDto
				cfg.CreateMap<Category, CategoryDto>();
				cfg.CreateMap<CategoryDto, Category>();

				//BlogPost ---- PostDto
				cfg.CreateMap<BlogPost, PostDto>();
				cfg.CreateMap<PostDto, BlogPost>();
			});

			MapperInstance = mappingConfigInstance.CreateMapper();
		}
	}
}
