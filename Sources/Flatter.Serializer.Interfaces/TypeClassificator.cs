using System;
using Flatter.Serializer.Models;

namespace Flatter.Serializer.Interfaces
{
    public interface ITypeClassificator
    {
        ETypeProperty GetPropertyType(Type type);
    }
}
