using System;
using System.Collections.Generic;
using Flatter.Serializer.Interfaces;

namespace Flatter.Serializer.Serializers
{
    public class SimpleSerializer: ISimpleSerializer
    {
        public SimpleSerializer(IKeyValuePairFactory keyValuePairFactory)
        {
            KeyValuePairFactory = keyValuePairFactory;
        }

        public KeyValuePair<string, object> SerializeObject(string prefix, object sender)
        {
            return KeyValuePairFactory.Create(prefix, sender);
        }

        public void DeserializeObject(KeyValuePair<string, object> property)
        {
            throw new NotImplementedException();
        }

        IKeyValuePairFactory KeyValuePairFactory { get; }
    }
}
