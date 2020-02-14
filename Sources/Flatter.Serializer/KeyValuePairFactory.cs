using System.Collections.Generic;
using Flatter.Serializer.Interfaces;

namespace Flatter.Serializer
{
    public class KeyValuePairFactory: IKeyValuePairFactory
    {
        public KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value) => new KeyValuePair<TKey, TValue>(key, value);
    }
}
