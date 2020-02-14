using System;
using System.Collections.Generic;
using System.Reflection;

namespace Flatter.Serializer.Interfaces
{
    public interface IPropertiesGetter
    {
        bool IsPrimitives(Type type);
        bool IsEnums(Type type);
        bool IsClasses(Type type);
        bool IsStrings(Type type);
        bool IsCollections(Type type);
        bool IsArrays(Type type);
        bool IsSimple(Type type);
        List<PropertyInfo> GetPrimitives(Type type);
        List<PropertyInfo> GetEnums(Type type);
        List<PropertyInfo> GetClasses(Type type);
        List<PropertyInfo> GetStrings(Type type);
        List<PropertyInfo> GetCollections(Type type);
        List<PropertyInfo> GetArrays(Type type);
    }
}
