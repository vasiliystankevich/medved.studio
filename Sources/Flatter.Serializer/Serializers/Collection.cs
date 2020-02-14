using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Flatter.Serializer.Interfaces;

namespace Flatter.Serializer.Serializers
{
    public class CollectionSerializer : RecursiveSerializer, ICollectionSerializer
    {
        public CollectionSerializer(IPrefixGenerator prefixGenerator, IPropertiesGetter propertiesGetter,
            ISerializerFactory serializerFactory) : base(prefixGenerator, propertiesGetter, serializerFactory)
        {
        }

        public override List<KeyValuePair<string, object>> SerializeObject(string prefix, Type type, object sender)
        {
            var result = new List<KeyValuePair<string, object>>();
            var properties = GetProperties(type);
            properties.ForEach(property => SerializeListProperty(prefix, sender, property, result));
            result.Sort((first, second) => string.Compare(first.Key, second.Key, StringComparison.OrdinalIgnoreCase));
            return result;
        }

        public override void DeserializeObject(List<KeyValuePair<string, object>> properties, Type type, object sender)
        {
            throw new NotImplementedException();
        }

        protected override List<PropertyInfo> GetProperties(Type type) => PropertiesGetter.GetCollections(type);

        protected virtual Type GetCollectionArgumentType(PropertyInfo property) =>
            property.PropertyType.GenericTypeArguments.First();


        private void SerializeListProperty(string prefix, object sender, PropertyInfo property, List<KeyValuePair<string, object>> result)
        {
            var elements = (IList)property.GetValue(sender);
            if (elements==null||elements.Count==0) return;
            var propertyPrefix = PrefixGenerator.Generate(prefix, property.Name);
            var collectionArgumentType = GetCollectionArgumentType(property);
            if (PropertiesGetter.IsSimple(collectionArgumentType))
                SerializeListSimpleElement(propertyPrefix, elements, result);
            else
                SerializeListComplexElement(propertyPrefix, elements, result);
        }

        private void SerializeListSimpleElement(string prefix, IList listElements, List<KeyValuePair<string, object>> result)
        {
            var index = 0;
            var simpleSerializator = SerializerFactory.GetSimpleSerializer();
            var data = from object element in listElements select simpleSerializator.SerializeObject($"{prefix}.[{index++}]", element);
            result.AddRange(data);
        }

        private void SerializeListComplexElement(string prefix, IList elements, List<KeyValuePair<string, object>> result)
        {
            var index = 0;
            var typeElement = elements[0].GetType();
            var serializator = SerializerFactory.GetSerializer(typeElement);
            foreach (var element in elements)
            {
                var data = serializator.SerializeObject($"{prefix}.[{index++}]", typeElement, element);
                result.AddRange(data);
            }
        }
    }
}
