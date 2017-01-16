using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.Infrastructure.Query;

namespace UniqueBlog.Repository
{
    public class QueryFactory
    {
        public static Query CreatePaginationQuery(PaginationQueryObject pageObject)
        {
            Query query = new Query("sp_get_items_super_pagination");
            query.Add(new Criterion("TableName", pageObject.TableName, CriterionOperator.Equal));
            query.Add(new Criterion("Fields", pageObject.Fields, CriterionOperator.Equal));
            query.Add(new Criterion("SqlWhere", pageObject.SqlWhere, CriterionOperator.Equal));
            query.Add(new Criterion("GroupFields", pageObject.GroupFileds, CriterionOperator.Equal));
            query.Add(new Criterion("OrderByFields", pageObject.OrderByFields, CriterionOperator.Equal));
            query.Add(new Criterion("PageIndex", pageObject.PageIndex, CriterionOperator.Equal));
            query.Add(new Criterion("PageSize", pageObject.PageSize, CriterionOperator.Equal));

            return query;
        }
    }
}
