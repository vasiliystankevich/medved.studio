using Flatter.Serializer.Interfaces;
using Flatter.Serializer.Interfaces.Unity;
using Flatter.Serializer.Unity;
using Project.Kernel;
using Unity;

namespace Flatter.Serializer
{
    public class TypeFabric : BaseTypeFabric
    {
        public override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUnityContainerExecutor, UnityContainerExecutor>();
            container.RegisterType<IUnityContainerFunctors, UnityContainerFunctors>();
            container.RegisterType<IRegistratorTypes, RegistratorTypes>();

            var registrator = container.Resolve<IRegistratorTypes>();
            registrator.RegisterAll();
        }
    }
}
