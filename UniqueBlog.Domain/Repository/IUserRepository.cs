using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;

namespace UniqueBlog.Domain.Repository
{
	/// <summary>
	/// User仓储接口
	/// </summary>
	public interface IUserRepository:IRepository<User>
	{
		
	}
}
