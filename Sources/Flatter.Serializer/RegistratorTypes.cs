using System;
using Flatter.Serializer.Interfaces;
using Flatter.Serializer.Interfaces.Unity;
using Flatter.Serializer.Serializers;

namespace Flatter.Serializer
{
    public class RegistratorTypes : IRegistratorTypes
    {
        public RegistratorTypes(IUnityContainerExecutor executor)
        {
            Executor = executor;
        }

        public void RegisterAll()
        {
            Executor.RegisterSingletonFactory<IAwaitTaskCreator>(executor => new AwaitTaskCreator());
            Executor.RegisterSingletonFactory<IPrefixGenerator>(executor => new PrefixGenerator());
            Executor.RegisterSingletonFactory<IKeyValuePairFactory>(executor => new KeyValuePairFactory());
            Executor.RegisterSingletonFactory<ITypeClassificator>(executor => new TypeClassificator());
            Executor.RegisterSingletonFactory<IPropertiesGetter>(executor => new PropertiesGetter());
            
            Executor.RegisterSingletonFactory<ISimpleSerializer>(executor =>
            {
                var propertiesGetter = Executor.Resolve<IKeyValuePairFactory>();
                return new SimpleSerializer(propertiesGetter);
            });

            RegisterBaseSerializer<IPrimitiveSerializer>((generator, getter, factory) => new PrimitiveSerializer(generator, getter, factory));
            RegisterBaseSerializer<IEnumSerializer>((generator, getter, factory) => new EnumSerializer(generator, getter, factory));
            RegisterBaseSerializer<IStringSerializer>((generator, getter, factory) => new StringSerializer(generator, getter, factory));

            RegisterRecursiveSerializer<ICollectionSerializer>((generator, getter, factory) => new CollectionSerializer(generator, getter, factory));
            RegisterRecursiveSerializer<IArraySerializer>((generator, getter, factory) => new ArraySerializer(generator, getter, factory));
            RegisterRecursiveSerializer<IClassSerializer>((generator, getter, factory) => new ClassSerializer(generator, getter, factory));

            Executor.RegisterSingletonFactory<ISerializerFactory>(executor =>
            {
                var typeClassificator = Executor.Resolve<ITypeClassificator>();
                return new SerializerFactory(executor, typeClassificator);
            });

            Executor.RegisterSingletonFactory<IFlatterConverter>(executor =>
            {
                var classSerializer = Executor.Resolve<IClassSerializer>();
                return new FlatterConverter(classSerializer);
            });

            Executor.RegisterSingletonFactory<IFlatterConverterAsync>(executor =>
            {
                var converter = Executor.Resolve<IFlatterConverter>();
                var taskCreator = Executor.Resolve<IAwaitTaskCreator>();
                return new FlatterConverterAsync(converter, taskCreator);
            });
        }

        void RegisterBaseSerializer<T>(Func<IPrefixGenerator, IPropertiesGetter, IKeyValuePairFactory, T> functor)
        {
            Executor.RegisterSingletonFactory<T>(executor =>
            {
                var generator = Executor.Resolve<IPrefixGenerator>();
                var getter = Executor.Resolve<IPropertiesGetter>();
                var factory = Executor.Resolve<IKeyValuePairFactory>();
                return functor(generator, getter, factory);
            });
        }

        void RegisterRecursiveSerializer<T>(Func<IPrefixGenerator, IPropertiesGetter, ISerializerFactory, T> functor)
        {
            Executor.RegisterSingletonFactory<T>(executor =>
            {
                var prefixGenerator = Executor.Resolve<IPrefixGenerator>();
                var propertiesGetter = Executor.Resolve<IPropertiesGetter>();
                var keyValuePairFactory = Executor.Resolve<ISerializerFactory>();
                return functor(prefixGenerator, propertiesGetter, keyValuePairFactory);
            });
        }

        IUnityContainerExecutor Executor { get; }
    }
}