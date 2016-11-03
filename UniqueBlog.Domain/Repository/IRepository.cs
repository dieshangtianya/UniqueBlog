using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Infrastructure;
using UniqueBlog.Infrastructure.Query;

namespace UniqueBlog.Domain.Repository
{
	public interface IRepository<T>
			where T : IAggregate
	{
		IEnumerable<T> FindAll();
		IEnumerable<T> FindAll(int index, int count);
		IEnumerable<T> FindBy(Query query);
		IEnumerable<T> FindBy(Query query, int index, int count);
		T FindBy(int entityId);
		void Add(T entity);
		void Save(T entity);
		void Remove(T entity);
	}
}
