using Charismatic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ITLand.CMMS.Libs.DevExtreme
{

    class DataSourceLoaderImpl<S>
    {
        readonly IQueryable Source;
        readonly DataSourceLoadContext Context;
        readonly Func<Expression, ExpressionExecutor> CreateExecutor;


        public DataSourceLoaderImpl(IQueryable source, CharismaticBaseListInputDto options, CancellationToken cancellationToken, bool sync)
        {
            var providerInfo = new QueryProviderInfo(source.Provider);

            Source = source;
            Context = new DataSourceLoadContext(options, providerInfo, Source.ElementType);
            CreateExecutor = expr => new ExpressionExecutor(Source.Provider, expr, providerInfo, cancellationToken, sync, true);

        }

        DataSourceExpressionBuilder CreateBuilder() => new DataSourceExpressionBuilder(Source.Expression, Context);

        public async Task<IQueryable<S>> LoadAsync()
        {
            Expression loadExpr;

            loadExpr = CreateBuilder().BuildLoadExpr(false);

            return await ExecExprAsync<S>(loadExpr);
        }

        async Task<IQueryable<R>> ExecExprAsync<R>(Expression expr)
        {

            var executor = CreateExecutor(expr);
            var result = await executor.ToQueryableAsync<R>();
            return result;
        }
    }

}
