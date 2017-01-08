using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Controllers.Constants
{
	public class ConstantData
	{
		public const string ProductName = "UQBlog";
		public const string TitleOfLogin = ProductName + "登录";
		public const string CurrentUserSessionKey = "CurrentUser";

		public const string BlogDataCacheKey = "blogdatainfo";
		public const string CategoryCacheKey = "categorylist";
	}
}
