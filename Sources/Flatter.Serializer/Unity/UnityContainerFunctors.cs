using System;
using Flatter.Serializer.Interfaces.Unity;
using Unity;

namespace Flatter.Serializer.Unity
{
    public class UnityContainerFunctors: IUnityContainerFunctors
    {
        public Func<IUnityContainer, object> GetUnityFactoryFunctor(IUnityContainerExecutor sender, Func<IUnityContainerExecutor, object> functor) => container => functor(sender);
    }
}
