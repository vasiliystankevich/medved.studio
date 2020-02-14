using System;
using System.Collections.Generic;
using System.Reflection;
using Flatter.Serializer.Interfaces;
using Flatter.Serializer.Models;

namespace Flatter.Serializer.Serializers
{
    public class ClassSerializer : RecursiveSerializer, IClassSerializer
    {
        public ClassSerializer(IPrefixGenerator prefixGenerator, IPropertiesGetter propertiesGetter, ISerializerFactory serializerFactory) : base(prefixGenerator, propertiesGetter, serializerFactory)
        {
        }

        public override List<KeyValuePair<string, object>> SerializeObject(string prefix, Type type, object sender)
        {
            var result = new List<KeyValuePair<string, object>>();
            var isAvaibleProperties = VerificationAvaibleProperties(type);
            SerializeSimpleProperties(prefix, type, sender, isAvaibleProperties, result);
            SerializeClassProperties(prefix, type, sender, isAvaibleProperties, result);
            return result;
        }

        public override void DeserializeObject(List<KeyValuePair<string, object>> properties, Type type, object sender)
        {
            throw new NotImplementedException();
        }

        void SerializeSimpleProperties(string prefix, Type type, object sender, int isAvaibleProperties, List<KeyValuePair<string, object>> result)
        {
            foreach (var typeProperty in PropertyTypes)
                if ((ETypeProperty) (isAvaibleProperties & (int) typeProperty) == typeProperty)
                {
                    var serializator = SerializerFactory.GetSerializer(typeProperty);
                    var data = serializator.SerializeObject(prefix, type, sender);
                    result.AddRange(data);
                }
        }

        void SerializeClassProperties(string prefix, Type type, object sender, int isAvaibleProperties,
            List<KeyValuePair<string, object>> result)
        {
            if ((ETypeProperty)(isAvaibleProperties & (int)ETypeProperty.Class) == ETypeProperty.None) return;
            var serializator = SerializerFactory.GetSerializer(ETypeProperty.Class);
            var properties = GetProperties(type);
            properties.ForEach(property =>
            {
                var propertyPrefix = PrefixGenerator.Generate(prefix, property.Name);
                var data = serializator.SerializeObject(propertyPrefix, property.PropertyType,
                    property.GetValue(sender));
                result.AddRange(data);
            });
        }

        int VerificationAvaibleProperties(Type type)
        {
            var isPrimitives = Convert.ToInt32(PropertiesGetter.IsPrimitives(type));
            var isEnums = Convert.ToInt32(PropertiesGetter.IsEnums(type));
            var isStrings = Convert.ToInt32(PropertiesGetter.IsStrings(type));
            var isCollections = Convert.ToInt32(PropertiesGetter.IsCollections(type));
            var isArrays = Convert.ToInt32(PropertiesGetter.IsArrays(type));
            var isClasses = Convert.ToInt32(PropertiesGetter.IsClasses(type));
            return (isClasses << 5) | (isArrays << 4) | (isCollections << 3) | (isStrings << 2) | (isEnums << 1) |
                   isPrimitives;
        }


        protected override List<PropertyInfo> GetProperties(Type type) => PropertiesGetter.GetClasses(type);

        private static readonly ETypeProperty[] PropertyTypes =
        {
            ETypeProperty.Primitive, ETypeProperty.Enum, ETypeProperty.String, ETypeProperty.Collection,
            ETypeProperty.Array,
        };
    }
}
