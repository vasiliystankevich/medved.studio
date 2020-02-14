using System;
using System.Collections;
using System.Linq;
using Flatter.Serializer.Interfaces;
using Flatter.Serializer.Models;

namespace Flatter.Serializer
{
    public class TypeClassificator : ITypeClassificator
    {
        public ETypeProperty GetPropertyType(Type type)
        {
            if (type.IsPrimitive) return ETypeProperty.Primitive;
            if (type.IsEnum) return ETypeProperty.Enum;
            if (type == typeof(string)) return ETypeProperty.String;
            if (type.IsArray) return ETypeProperty.Array;
            if (IsGenericCollection(type)) return ETypeProperty.Collection;
            return ETypeProperty.Class;
        }

        bool IsGenericCollection(Type type)
        {
            var interfaces = type.GetInterfaces();
            var isCollection = interfaces.Any(element => element == typeof(ICollection));
            var isList = interfaces.Any(element => element == typeof(IList));
            return !type.IsArray && isCollection && isList;
        }
    }
}
