using System;
using System.Collections.Generic;

namespace Flatter.Serializer.Interfaces
{
    public interface ISerializer
    {
        List<KeyValuePair<string, object>> SerializeObject(string prefix, Type type, object sender);
        void DeserializeObject(List<KeyValuePair<string, object>> properties, Type type, object sender);
    }

    public interface ISimpleSerializer
    {
        KeyValuePair<string, object> SerializeObject(string prefix, object sender);
        void DeserializeObject(KeyValuePair<string, object> property);
    }

    public interface IPrimitiveSerializer : ISerializer { }
    public interface IEnumSerializer : ISerializer { }
    public interface IStringSerializer : ISerializer { }
    public interface ICollectionSerializer : ISerializer { }
    public interface IArraySerializer : ISerializer { }
    public interface IClassSerializer : ISerializer { }
}
