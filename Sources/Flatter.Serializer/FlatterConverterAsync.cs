using System;
using System.Threading;
using System.Threading.Tasks;
using Flatter.Serializer.Interfaces;
using Flatter.Serializer.Models;

namespace Flatter.Serializer
{
    public class FlatterConverterAsync : IFlatterConverterAsync
    {
        public FlatterConverterAsync(IFlatterConverter flatterConverter, IAwaitTaskCreator taskCreator)
        {
            FlatterConverter = flatterConverter;
            TaskCreator = taskCreator;
        }

        public async Task<FlatterResult> SerializeObjectAsync(object data, CancellationToken cancellationToken,
            bool continueOnCapturedContext) => await TaskCreator.Create(data, FlatterConverter.SerializeObject,
            cancellationToken, continueOnCapturedContext);

        public Task<T> DeserializeObjectAsync<T>(FlatterResult result, CancellationToken cancellationToken,
            bool continueOnCapturedContext) => throw new NotImplementedException();

        IFlatterConverter FlatterConverter { get; }
        IAwaitTaskCreator TaskCreator { get; }
    }
}
