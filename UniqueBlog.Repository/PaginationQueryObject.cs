using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Repository
{
    /// <summary>
    /// Pagination object which used to present data about paging
    /// </summary>
    public class PaginationQueryObject
    {
        public string TableName { get; set; }

        public string Fields { get; set; }

        public string SqlWhere { get; set; }

        public string GroupFileds { get; set; }

        public string OrderByFields { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public PaginationQueryObject()
            : this(null)
        {

        }

        public PaginationQueryObject(string tableName)
        {
            this.TableName = tableName;
            this.Fields = "*";
            this.SqlWhere = "";
        }
    }
}
