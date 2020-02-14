using System;
using Flatter.Serializer.Models;

namespace Flatter.Serializer.Interfaces
{
    public interface ISerializerFactory
    {
        ISerializer GetSerializer(Type type);
        ISerializer GetSerializer(ETypeProperty propertryType);
        ISimpleSerializer GetSimpleSerializer();
    }
}
