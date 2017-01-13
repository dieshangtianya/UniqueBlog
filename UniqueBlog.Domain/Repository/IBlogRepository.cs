using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;

namespace UniqueBlog.Domain.Repository
{
	/// <summary>
	/// Blog仓储接口
	/// </summary>
	public interface IBlogRepository:IRepository<Blog>
	{
	}
}
