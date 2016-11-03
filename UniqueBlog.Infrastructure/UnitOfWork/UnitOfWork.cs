using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace UniqueBlog.Infrastructure.UnitOfWork
{
	[Export(typeof(IUnitOfWork))]
	public class UnitOfWork : IUnitOfWork
	{
		private Dictionary<IAggregate, IUnitOfWorkRepository> addedEntities;
		private Dictionary<IAggregate, IUnitOfWorkRepository> changedEntities;
		private Dictionary<IAggregate, IUnitOfWorkRepository> deletedEntities;

		public UnitOfWork()
		{
			addedEntities = new Dictionary<IAggregate, IUnitOfWorkRepository>();
			changedEntities = new Dictionary<IAggregate, IUnitOfWorkRepository>();
			deletedEntities = new Dictionary<IAggregate, IUnitOfWorkRepository>();
		}

		/// <summary>
		/// 注册要在Unit Of Work中的新增操作
		/// </summary>
		/// <param name="entity">将要新增的对象实体</param>
		/// <param name="unitOfWorkRepository">用于新增的Repository对象</param>
		public void RegisterNew(IAggregate entity, IUnitOfWorkRepository unitOfWorkRepository)
		{
			if(!addedEntities.ContainsKey(entity))
			{
				addedEntities.Add(entity, unitOfWorkRepository);
			}
		}

		/// <summary>
		/// 注册要在Unit Of Work中的更新操作
		/// </summary>
		/// <param name="entity">将要更新的对象实体</param>
		/// <param name="unitOfWorkRepository">用于更新的Repository对象</param>
		public void RegisterAmended(IAggregate entity, IUnitOfWorkRepository unitOfWorkRepository)
		{
			if (!changedEntities.ContainsKey(entity)) 
			{
				changedEntities.Add(entity, unitOfWorkRepository);
			}
		}

		/// <summary>
		/// 注册要在Unit Of Work中的更新操作
		/// </summary>
		/// <param name="entity">将要删除的对象实体</param>
		/// <param name="unitWorkRepository">用于删除的Repository对象</param>
		public void RegisterRemoved(IAggregate entity, IUnitOfWorkRepository unitWorkRepository)
		{
			if(!deletedEntities.ContainsKey(entity))
			{
				deletedEntities.Add(entity, unitWorkRepository);
			}
		}

		/// <summary>
		/// 提交Unit Of Work里的所有对象操作
		/// </summary>
		public void Commit()
		{
			using (TransactionScope transactionScope = new TransactionScope()) 
			{
				foreach(var entity in this.addedEntities.Keys)
				{
					this.addedEntities[entity].PersistCreationOf(entity);
				}

				foreach(var entity in this.changedEntities.Keys)
				{
					this.changedEntities[entity].PersistUpdateOf(entity);
				}

				foreach(var entity in this.deletedEntities.Keys)
				{
					this.deletedEntities[entity].PersistDeleteOf(entity);
				}

				transactionScope.Complete();
			}
		}
	}
}
