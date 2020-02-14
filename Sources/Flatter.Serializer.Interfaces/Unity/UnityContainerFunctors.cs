using System;
using Unity;

namespace Flatter.Serializer.Interfaces.Unity
{
    public interface IUnityContainerFunctors
    {
        Func<IUnityContainer, object> GetUnityFactoryFunctor(IUnityContainerExecutor sender,
            Func<IUnityContainerExecutor, object> functor);
    }
}
