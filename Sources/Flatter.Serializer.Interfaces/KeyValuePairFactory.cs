using System.Collections.Generic;

namespace Flatter.Serializer.Interfaces
{
    public interface IKeyValuePairFactory
    {
        KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value);
    }
}
