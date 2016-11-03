using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.UnitOfWork
{
	/// <summary>
	/// IUnitOfWorkRepository接口用于在Unit Of Work中的Repository需要实现的接口
	/// </summary>
	public interface IUnitOfWorkRepository
	{
		/// <summary>
		/// 持久化Create操作
		/// </summary>
		/// <param name="entity">实体对象</param>
		void PersistCreationOf(IAggregate entity);

		/// <summary>
		/// 持久化Update操作
		/// </summary>
		/// <param name="entity">实体对象</param>
		void PersistUpdateOf(IAggregate entity);

		/// <summary>
		/// 持久化Delete操作
		/// </summary>
		/// <param name="entity">实体对象</param>
		void PersistDeleteOf(IAggregate entity);
	}
}
