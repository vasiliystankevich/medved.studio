using System;
using Flatter.Serializer.Interfaces;
using Flatter.Serializer.Models;

namespace Flatter.Serializer
{
    public class FlatterConverter: IFlatterConverter
    {
        public FlatterConverter(IClassSerializer serializer)
        {
            Serializer = serializer;
        }

        public FlatterResult SerializeObject(object data)
        {
            var objectType = data.GetType();
            var values = Serializer.SerializeObject(string.Empty, objectType, data);
            return new FlatterResult(values);
        }           

        public T DeserializeObject<T>(FlatterResult result) => throw new NotImplementedException();

        IClassSerializer Serializer { get; }
    }
}