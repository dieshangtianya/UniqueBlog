using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.UnitOfWork
{
	/// <summary>
	/// IUnitOfWorkRepository should be implemented in the repositories which would be call in a unit of work
	/// </summary>
	public interface IUnitOfWorkRepository
	{
        /// <summary>
        /// Set the unit of work instance
        /// </summary>
        /// <param name="unitOfWork">instance of IUnitOfWork</param>
        void SetUnitOfWork(IUnitOfWork unitOfWork);

        /// <summary>
        /// Persist the creation operation
        /// </summary>
        /// <param name="entity">entity</param>
        void PersistCreationOf(IAggregateRoot entity);

		/// <summary>
		/// Persist the update operation
		/// </summary>
		/// <param name="entity">entity</param>
		void PersistUpdateOf(IAggregateRoot entity);

		/// <summary>
		/// Persist the delete operation
		/// </summary>
		/// <param name="entity">entity</param>
		void PersistDeleteOf(IAggregateRoot entity);
	}
}
