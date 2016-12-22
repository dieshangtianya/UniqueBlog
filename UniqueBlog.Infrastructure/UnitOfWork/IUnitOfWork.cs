using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.UnitOfWork
{
	/// <summary>
	/// IUnitOfWork is used to register the Add/Update/Delete operations
	/// </summary>
	public interface IUnitOfWork
	{
		/// <summary>
		/// Register the addtion in the unit of work
		/// </summary>
		/// <param name="entity">Entity will be added</param>
		/// <param name="unitOfWorkRepository">Repository used to execute the add operation</param>
		void RegisterNew(IAggregate entity, IUnitOfWorkRepository unitOfWorkRepository);

        /// <summary>
        /// Register the amendment in the unit of work
        /// </summary>
        /// <param name="entity">Entity will be updated</param>
        /// <param name="unitOfWorkRepository">Repository used to execute the amendment operation</param>
        void RegisterAmended(IAggregate entity, IUnitOfWorkRepository unitOfWorkRepository);

        /// <summary>
        /// Register the removement in the unit of work
        /// </summary>
        /// <param name="entity">>Entity will be deleted</param>
        /// <param name="unitWorkRepository">Repository used to execute the removement operation</param>
        void RegisterRemoved(IAggregate entity, IUnitOfWorkRepository unitWorkRepository);

		/// <summary>
		/// Commit all the operations in a unit of work
		/// </summary>
		void Commit();
	}
}
