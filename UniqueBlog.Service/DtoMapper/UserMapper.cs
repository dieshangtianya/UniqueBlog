using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;
using UniqueBlog.DTO;

namespace UniqueBlog.Service.DtoMapper
{
	public static class UserMapper
	{
		public static UserDto ConvertTo(this User user)
		{
			return DtoMapper.AutoMapperConfig.MapperInstance.Map<User, UserDto>(user);
		}
	}
}
