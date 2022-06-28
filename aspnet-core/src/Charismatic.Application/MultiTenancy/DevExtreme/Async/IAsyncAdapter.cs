using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ITLand.CMMS.Libs.DevExtreme.Async
{

    public interface IAsyncAdapter
    {
        Task<IQueryable<T>> ToQueryableAsync<T>(IQueryProvider queryProvider, Expression expr, CancellationToken cancellationToken);
    }

}
