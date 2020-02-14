using System;
using Flatter.Serializer.Interfaces;
using Flatter.Serializer.Interfaces.Unity;
using Flatter.Serializer.Models;

namespace Flatter.Serializer
{
    public class SerializerFactory:ISerializerFactory
    {
        public SerializerFactory(IUnityContainerExecutor executor, ITypeClassificator typeClassificator)
        {
            Executor = executor;
            TypeClassificator = typeClassificator;
        }

        public ISerializer GetSerializer(Type type)
        {
            var propertyType = TypeClassificator.GetPropertyType(type);
            return GetSerializer(propertyType);
        }

        public ISerializer GetSerializer(ETypeProperty propertyType)
        {
            ISerializer result = null;
            switch (propertyType)
            {
                case ETypeProperty.Primitive: result = Executor.Resolve<IPrimitiveSerializer>();break;
                case ETypeProperty.Enum: result = Executor.Resolve<IEnumSerializer>(); break;
                case ETypeProperty.String: result = Executor.Resolve<IStringSerializer>(); break;
                case ETypeProperty.Collection: result = Executor.Resolve<ICollectionSerializer>(); break;
                case ETypeProperty.Array: result = Executor.Resolve<IArraySerializer>(); break;
                case ETypeProperty.Class: result = Executor.Resolve<IClassSerializer>(); break;
            }
            return result;
        }

        public ISimpleSerializer GetSimpleSerializer()
        {
            return Executor.Resolve<ISimpleSerializer>();
        }

        IUnityContainerExecutor Executor { get; }
        ITypeClassificator TypeClassificator { get; }
    }
}
