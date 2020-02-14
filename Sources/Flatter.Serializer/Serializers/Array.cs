using System;
using System.Collections.Generic;
using System.Reflection;
using Flatter.Serializer.Interfaces;

namespace Flatter.Serializer.Serializers
{
    public class ArraySerializer : CollectionSerializer, IArraySerializer
    {
        public ArraySerializer(IPrefixGenerator prefixGenerator, IPropertiesGetter propertiesGetter,
            ISerializerFactory serializerFactory) : base(prefixGenerator, propertiesGetter, serializerFactory)
        {
        }

        protected override List<PropertyInfo> GetProperties(Type type) => PropertiesGetter.GetArrays(type);

        protected override Type GetCollectionArgumentType(PropertyInfo property) =>
            property.PropertyType.GetElementType();

    }
}
