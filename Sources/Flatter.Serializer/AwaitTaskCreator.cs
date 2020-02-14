using System;
using System.Threading;
using System.Threading.Tasks;
using Flatter.Serializer.Interfaces;

namespace Flatter.Serializer
{
    public class AwaitTaskCreator: IAwaitTaskCreator
    {
        public async Task<TResult> Create<TData, TResult>(TData data, Func<TData, TResult> functor,
            CancellationToken cancellationToken, bool continueOnCapturedContext)
        {
            return await Task<TResult>.Factory.StartNew(() => functor(data), cancellationToken)
                .ConfigureAwait(continueOnCapturedContext);
        }
    }
}