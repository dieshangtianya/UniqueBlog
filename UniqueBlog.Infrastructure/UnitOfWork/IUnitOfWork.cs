using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.UnitOfWork
{
	/// <summary>
	/// IUnitOfWork接口用于注册Add/Update/Delete操作
	/// </summary>
	public interface IUnitOfWork
	{
		/// <summary>
		/// 注册要在Unit Of Work中的新增操作
		/// </summary>
		/// <param name="entity">将要新增的对象实体</param>
		/// <param name="unitOfWorkRepository">用于新增的Repository对象</param>
		void RegisterNew(IAggregate entity, IUnitOfWorkRepository unitOfWorkRepository);

		/// <summary>
		/// 注册要在Unit Of Work中的更新操作
		/// </summary>
		/// <param name="entity">将要更新的对象实体</param>
		/// <param name="unitOfWorkRepository">用于更新的Repository对象</param>
		void RegisterAmended(IAggregate entity, IUnitOfWorkRepository unitOfWorkRepository);

		/// <summary>
		/// 注册要在Unit Of Work中的更新操作
		/// </summary>
		/// <param name="entity">将要删除的对象实体</param>
		/// <param name="unitWorkRepository">用于删除的Repository对象</param>
		void RegisterRemoved(IAggregate entity, IUnitOfWorkRepository unitWorkRepository);

		/// <summary>
		/// 提交Unit Of Work里的所有对象操作
		/// </summary>
		void Commit();
	}
}
