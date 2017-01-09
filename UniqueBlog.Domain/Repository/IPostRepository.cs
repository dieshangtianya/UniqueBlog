using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Infrastructure.Query;

namespace UniqueBlog.Domain.Repository
{
	public interface IPostRepository:IRepository<BlogPost>
	{
        int GetPostAmount(Query query);
	}
}
