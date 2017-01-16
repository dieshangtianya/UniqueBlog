using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UniqueBlog.DBManager;
using UniqueBlog.Infrastructure.Query;

namespace UniqueBlog.Repository
{
    public static class QueryTranslator
    {

        public static string TranslateIntoSql(this Query query)
        {
            string sqlQuery = GenerateSqlWhereClause(query);

            sqlQuery += GenerateOrderByClause(query);

            return sqlQuery;
        }

        public static void TranslateIntoSql(this Query query, DbCommand command, string baseQuery = null)
        {


            if (query.IsNamedQuery())
            {
                command.CommandText = query.QueryName;
                command.CommandType = CommandType.StoredProcedure;

                GenerateParameterCollections(query, command);

                return;
            }

            command.CommandType = CommandType.Text;
            command.CommandText = baseQuery + TranslateIntoSql(query);
            GenerateParameterCollections(query, command);
        }

        public static string TranslateIntoWhereSql(this Query query)
        {
            return GeneratePlainSqlWhereClause(query);
        }

        private static string GenerateSqlWhereClause(Query query)
        {
            StringBuilder sqlBuilder = new StringBuilder(" where 1=1 ");

            string queryOperator = GetQueryOperator(query.QueryOperator);

            if (query.Criteria.Count() != 0)
            {
                foreach (var criterion in query.Criteria)
                {
                    sqlBuilder.Append(queryOperator);
                    sqlBuilder.Append(GetFilterClause(criterion));
                }
            }

            return sqlBuilder.ToString();
        }

        private static string GeneratePlainSqlWhereClause(Query query)
        {
            StringBuilder sqlBuilder = new StringBuilder(" where 1=1 ");

            string queryOperator = GetQueryOperator(query.QueryOperator);

            if (query.Criteria.Count() != 0)
            {
                foreach (var criterion in query.Criteria)
                {
                    sqlBuilder.Append(queryOperator);
                    sqlBuilder.Append(GetPlainFilterClause(criterion));
                }
            }

            return sqlBuilder.ToString();
        }

        private static string GenerateOrderByClause(Query query)
        {
            if (query.OrderByClause != null)
            {
                return string.Format(" ORDER BY {0} {1}",
                query.OrderByClause.PropertyName,
                query.OrderByClause.Desc ? "DESC" : "ASC");
            }
            return string.Empty;
        }

        private static string GetQueryOperator(QueryOperator queryOperator)
        {
            switch (queryOperator)
            {
                case QueryOperator.And:
                    return " AND ";
                case QueryOperator.Or:
                    return " OR ";
                default:
                    throw new ArgumentException("QueryOperator is an invalid value");
            }
        }

        private static string GetSqlCriterionOperator(CriterionOperator criterionOperator)
        {
            switch (criterionOperator)
            {
                case CriterionOperator.Equal:
                    return "=";
                case CriterionOperator.GreaterThan:
                    return ">";
                case CriterionOperator.GreaterThanOrEqual:
                    return ">=";
                case CriterionOperator.LessThan:
                    return "<";
                case CriterionOperator.LessThanOrEqual:
                    return "<=";
                case CriterionOperator.Like:
                    return "like";
                default:
                    throw new ApplicationException("No criterion operator defined");
            }
        }

        private static string GetFilterClause(Criterion criterion)
        {
            return string.Format("{0} {1} @{2}",
                criterion.PropertyName,
                GetSqlCriterionOperator(criterion.CriterionOperator),
                criterion.PropertyName);
        }

        private static string GetPlainFilterClause(Criterion criterion)
        {
            return string.Format("{0} {1} {2}",
                criterion.PropertyName,
                GetSqlCriterionOperator(criterion.CriterionOperator),
                GetFilterText(criterion.Value));
        }

        private static string GetFilterText(object value)
        {
            Type valueType = value.GetType();
            if (valueType == typeof(int) || valueType == typeof(double) || valueType == typeof(float) || valueType == typeof(decimal))
            {
                return value.ToString();
            }
            else
            {
                return "'" + value.ToString() + "'";
            }

        }

        private static void GenerateParameterCollections(Query query, DbCommand command)
        {
            IDatabase iDatabase = DatabaseFactory.CreateDataBase();

            foreach (Criterion criterion in query.Criteria)
            {
                DbParameter parameter = iDatabase.CreateDbParameter(criterion.PropertyName, criterion.Value);

                command.Parameters.Add(parameter);
            }
        }

    }
}
