using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Flatter.Serializer.Interfaces;

namespace Flatter.Serializer.Serializers
{
    public abstract class BaseSerializer:ISerializer
    {
        protected BaseSerializer(IPrefixGenerator prefixGenerator, IPropertiesGetter propertiesGetter, IKeyValuePairFactory keyValuePairFactory)
        {
            PrefixGenerator = prefixGenerator;
            PropertiesGetter = propertiesGetter;
            KeyValuePairFactory = keyValuePairFactory;
        }

        public virtual List<KeyValuePair<string, object>> SerializeObject(string prefix, Type type, object sender)
        {
            var properties = GetProperties(type);
            return properties.Select(property =>
            {
                var key = PrefixGenerator.Generate(prefix, property.Name);
                return KeyValuePairFactory.Create(key, property.GetValue(sender));
            }).ToList();
        }

        public void DeserializeObject(List<KeyValuePair<string, object>> properties, Type type, object sender)
        {
            throw new NotImplementedException();
        }

        protected abstract List<PropertyInfo> GetProperties(Type type);

        protected IPrefixGenerator PrefixGenerator { get; }
        protected IPropertiesGetter PropertiesGetter { get; }
        protected IKeyValuePairFactory KeyValuePairFactory { get; }

    }
}