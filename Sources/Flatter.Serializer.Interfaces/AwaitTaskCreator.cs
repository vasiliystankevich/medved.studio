using System;
using System.Threading;
using System.Threading.Tasks;

namespace Flatter.Serializer.Interfaces
{
    public interface IAwaitTaskCreator
    {
        Task<TResult> Create<TData, TResult>(TData data, Func<TData, TResult> functor,
            CancellationToken cancellationToken, bool continueOnCapturedContext);
    }
}
