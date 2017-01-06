using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Infrastructure;
using UniqueBlog.Infrastructure.Query;

namespace UniqueBlog.Domain.Repository
{
    public interface IRepository<TEntity>
            where TEntity : EntityBase, IAggregateRoot
    {
        IEnumerable<TEntity> FindAll();
        IEnumerable<TEntity> FindAll(int index, int count);
        IEnumerable<TEntity> FindBy(Query query);
        IEnumerable<TEntity> FindBy(Query query, int index, int count);
        void Add(TEntity entity);
        void Save(TEntity entity);
        void Remove(TEntity entity);
    }
}
