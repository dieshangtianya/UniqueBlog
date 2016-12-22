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
        /// Register the addtion in the unit of work
        /// </summary>
        /// <param name="entity">Entity will be added</param>
        /// <param name="unitOfWorkRepository">Repository used to execute the add operation</param>
        public void RegisterNew(IAggregate entity, IUnitOfWorkRepository unitOfWorkRepository)
		{
			if(!addedEntities.ContainsKey(entity))
			{
				addedEntities.Add(entity, unitOfWorkRepository);
			}
		}

        /// <summary>
        /// Register the amendment in the unit of work
        /// </summary>
        /// <param name="entity">Entity will be updated</param>
        /// <param name="unitOfWorkRepository">Repository used to execute the amendment operation</param>
        public void RegisterAmended(IAggregate entity, IUnitOfWorkRepository unitOfWorkRepository)
		{
			if (!changedEntities.ContainsKey(entity)) 
			{
				changedEntities.Add(entity, unitOfWorkRepository);
			}
		}

        /// <summary>
        /// Register the removement in the unit of work
        /// </summary>
        /// <param name="entity">>Entity will be deleted</param>
        /// <param name="unitWorkRepository">Repository used to execute the removement operation</param>
        public void RegisterRemoved(IAggregate entity, IUnitOfWorkRepository unitWorkRepository)
		{
			if(!deletedEntities.ContainsKey(entity))
			{
				deletedEntities.Add(entity, unitWorkRepository);
			}
		}

        /// <summary>
        /// Commit all the operations in a unit of work
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
