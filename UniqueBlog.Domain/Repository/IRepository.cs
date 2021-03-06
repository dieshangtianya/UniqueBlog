﻿using System;
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
        PagedResult<TEntity> FindAll(int pageIndex, int pageSize);
        IEnumerable<TEntity> FindBy(Query query);
        PagedResult<TEntity> FindBy(Query query, int pageIndex, int pageSize);
        void Add(TEntity entity);
        void Save(TEntity entity);
        void Remove(TEntity entity);
    }
}
