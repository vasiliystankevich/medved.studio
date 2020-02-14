using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Flatter.Serializer.Interfaces;

namespace Flatter.Serializer
{
    public class PropertiesGetter: IPropertiesGetter
    {
        public bool IsPrimitives(Type type) => Get(type).Any(property => property.PropertyType.IsPrimitive);

        public bool IsEnums(Type type) => Get(type).Any(property => property.PropertyType.IsEnum);

        public bool IsClasses(Type type) => GetAllClasses(type).Any(property => !IsSimpleClass(property.PropertyType));

        public bool IsStrings(Type type) => GetAllClasses(type).Any(property => property.PropertyType == typeof(string));

        public bool IsCollections(Type type) => GetAllClasses(type).Any(property => IsGenericCollection(property.PropertyType));

        public bool IsArrays(Type type) => Get(type).Any(property => property.PropertyType.IsArray);

        public bool IsSimple(Type type)
        {
            var isPrimitive = type.IsPrimitive;
            var isEnum = type.IsEnum;
            var isString = type == typeof(string);
            return isPrimitive | isEnum | isString;
        }

        public List<PropertyInfo> GetPrimitives(Type type) => Get(type).Where(property => property.PropertyType.IsPrimitive).ToList();

        public List<PropertyInfo> GetEnums(Type type) => Get(type).Where(property => property.PropertyType.IsEnum).ToList();

        public List<PropertyInfo> GetClasses(Type type) => GetAllClasses(type).Where(property => !IsSimpleClass(property.PropertyType)).ToList();

        public List<PropertyInfo> GetStrings(Type type) => GetAllClasses(type).Where(property => property.PropertyType == typeof(string)).ToList();

        public List<PropertyInfo> GetCollections(Type type) => GetAllClasses(type).Where(property => IsGenericCollection(property.PropertyType)).ToList();
        
        public List<PropertyInfo> GetArrays(Type type) => Get(type).Where(property => property.PropertyType.IsArray).ToList();

        IEnumerable<PropertyInfo> Get(Type type) => type.GetProperties();

        bool IsGenericCollection(Type type)
        {
            var interfaces = type.GetInterfaces();
            var isCollection = interfaces.Any(element => element == typeof(ICollection));
            var isList = interfaces.Any(element => element == typeof(IList));
            return !type.IsArray && isCollection && isList;
        }

        bool IsSimpleClass(Type type) => type.IsArray | type == typeof(string) | IsGenericCollection(type);
        IEnumerable<PropertyInfo> GetAllClasses(Type type) => Get(type).Where(property => property.PropertyType.IsClass);
    }
}
