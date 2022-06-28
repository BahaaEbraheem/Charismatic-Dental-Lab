using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ITLand.CMMS.Libs.DevExtreme
{
    class DataSourceExpressionBuilder
    {
        Expression Expr;
        readonly DataSourceLoadContext Context;

        public DataSourceExpressionBuilder(Expression expr, DataSourceLoadContext context)
        {
            Expr = expr;
            Context = context;
        }

        public Expression BuildLoadExpr(bool paginate, IList filterOverride = null, IReadOnlyList<string> selectOverride = null)
        {
            AddFilter(filterOverride);
            return Expr;
        }


        void AddFilter(IList filterOverride = null)
        {
            if (filterOverride != null || Context.HasFilter)
            {
                var filterExpr = filterOverride != null && filterOverride.Count < 1
                    ? Expression.Lambda(Expression.Constant(false), Expression.Parameter(GetItemType()))
                    : new FilterExpressionCompiler(GetItemType(), Context.GuardNulls, Context.UseStringToLower).Compile(filterOverride ?? Context.Filter);

                Expr = QueryableCall(nameof(Queryable.Where), Expression.Quote(filterExpr));
            }
        }

        Expression QueryableCall(string methodName, Expression arg)
            => Expression.Call(typeof(Queryable), methodName, GetQueryableGenericArguments(), Expr, arg);

        Type[] GetQueryableGenericArguments()
        {
            const string queryable1 = "IQueryable`1";
            var type = Expr.Type;

            if (type.IsInterface && type.Name == queryable1)
                return type.GenericTypeArguments;

            return type.GetInterface(queryable1).GenericTypeArguments;
        }

        Type GetItemType()
            => GetQueryableGenericArguments().First();

    }

}
