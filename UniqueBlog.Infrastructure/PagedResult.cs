using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure
{
    public class PagedResult<TEntity>
    {
        public int TotalRecordsCount { get; private set; }

        public IEnumerable<TEntity> Items { get; private set; }

        public PagedResult(int totalRecordCount,IEnumerable<TEntity> items)
        {
            this.TotalRecordsCount = totalRecordCount;
            this.Items = items;
        }
    }
}
