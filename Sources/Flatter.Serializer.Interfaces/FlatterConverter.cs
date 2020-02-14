using System.Threading;
using System.Threading.Tasks;
using Flatter.Serializer.Models;

namespace Flatter.Serializer.Interfaces
{
    public interface IFlatterConverter
    {
        FlatterResult SerializeObject(object data);
        T DeserializeObject<T>(FlatterResult result);
    }

    public interface IFlatterConverterAsync
    {
        Task<FlatterResult> SerializeObjectAsync(object data, CancellationToken cancellationToken, bool continueOnCapturedContext);
        Task<T> DeserializeObjectAsync<T>(FlatterResult result, CancellationToken cancellationToken, bool continueOnCapturedContext);
    }
}
