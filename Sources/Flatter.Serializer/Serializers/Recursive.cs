using System;
using System.Collections.Generic;
using System.Reflection;
using Flatter.Serializer.Interfaces;

namespace Flatter.Serializer.Serializers
{
    public abstract class RecursiveSerializer : ISerializer
    {
        protected RecursiveSerializer(IPrefixGenerator prefixGenerator, IPropertiesGetter propertiesGetter, ISerializerFactory serializerFactory)
        {
            PrefixGenerator = prefixGenerator;
            PropertiesGetter = propertiesGetter;
            SerializerFactory = serializerFactory;
        }

        public abstract List<KeyValuePair<string, object>> SerializeObject(string prefix, Type type, object sender);
        public abstract void DeserializeObject(List<KeyValuePair<string, object>> properties, Type type, object sender);
        protected virtual List<PropertyInfo> GetProperties(Type type) => null;

        protected IPrefixGenerator PrefixGenerator { get; }
        protected IPropertiesGetter PropertiesGetter { get; }
        protected ISerializerFactory SerializerFactory { get; }
    }
}
