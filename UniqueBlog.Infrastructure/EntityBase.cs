using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure
{
	/// <summary>
    /// Entity base class which represent the entity
    /// It suppose some other common functionalities, such as Id property
    /// </summary>
	public abstract class EntityBase
	{
        protected EntityBase(int id)
        {
            this.Id = id;
        }

        public int Id { get; private set; }

        #region Override and overload Equal method

        public override bool Equals(object entity)
        {
            return this.Equals(entity as EntityBase);
        }

        public bool Equals(EntityBase entity)
        {
            if (ReferenceEquals(entity, null))
                return false;

            if (ReferenceEquals(this, entity))
                return true;

            //if both current instance and entity are transient, then compare their reference
            var entityIsTransient = entity.Id == default(int);
            if (IsTransient() && entityIsTransient)
            {
                return ReferenceEquals(this, entity);
            }

            if (!IsTransient() && this.Id == entity.Id)
                return true;

            return false;
        }

        #endregion

        #region Override GatHashCode method

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #endregion

        #region Overload operator of "==" and "!="
        public static bool operator ==(EntityBase entityA,EntityBase entityB)
        {
            if (ReferenceEquals(entityA, null) && ReferenceEquals(entityB, null))
                return true;
            if (ReferenceEquals(entityA, null) || ReferenceEquals(entityB, null))
                return false;

            return entityA.Equals(entityB);
        }

        public static bool operator !=(EntityBase entityA,EntityBase entityB)
        {
            return !(entityA == entityB);
        }
        #endregion

        /// <summary>
        /// Determine whether the entity is transient
        /// </summary>
        /// <returns></returns>
        public bool IsTransient()
        {
            return this.Id == default(int);
        }

        public static void SetId(EntityBase entity,int id)
        {
            entity.Id = id;
        }
    }
}
