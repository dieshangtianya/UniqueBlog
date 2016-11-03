using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Service.DtoMapper;

namespace UniqueBlog.Controllers.DtoMapperManager
{
	public static class MapperManager
	{
		public static void RegisterTypeMapper()
		{
			AutoMapperConfig.InitializeTypeMapper();
		}
	}
}
