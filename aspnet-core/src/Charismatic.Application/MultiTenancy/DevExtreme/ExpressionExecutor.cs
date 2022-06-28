using ITLand.CMMS.Libs.DevExtreme.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ITLand.CMMS.Libs.DevExtreme
{

    class ExpressionExecutor
    {
        IQueryProvider Provider;
        Expression Expr;

        readonly QueryProviderInfo ProviderInfo;
        readonly CancellationToken CancellationToken;
        readonly bool Sync;
        readonly bool AllowAsyncOverSync;

        public ExpressionExecutor(IQueryProvider provider, Expression expr, QueryProviderInfo providerInfo, CancellationToken cancellationToken, bool sync, bool allowAsyncOverSync)
        {
            Provider = provider;
            Expr = expr;

            ProviderInfo = providerInfo;
            CancellationToken = cancellationToken;
            Sync = sync;
            AllowAsyncOverSync = allowAsyncOverSync;
        }

        public Task<IQueryable<T>> ToQueryableAsync<T>()
        {
            CancellationToken.ThrowIfCancellationRequested();
            return CreateAsyncAdapter().ToQueryableAsync<T>(Provider, Expr, CancellationToken);
        }

        Async.IAsyncAdapter CreateAsyncAdapter()
        {
            var providerType = Provider.GetType();

            return AsyncOverSyncAdapter.Instance;
        }

    }

}
