using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ITLand.CMMS.Libs.DevExtreme.Async
{

    class AsyncOverSyncAdapter : IAsyncAdapter
    {
        public static readonly IAsyncAdapter Instance = new AsyncOverSyncAdapter();

        private AsyncOverSyncAdapter()
        {
        }

        // NOTE on Task.FromResult vs. Task.Run https://stackoverflow.com/a/34005461

        Task<IQueryable<T>> IAsyncAdapter.ToQueryableAsync<T>(IQueryProvider queryProvider, Expression expr, CancellationToken _)
            => Task.FromResult(queryProvider.CreateQuery<T>(expr));
    }

}
