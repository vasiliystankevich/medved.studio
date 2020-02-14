using System;
using Unity.Lifetime;

namespace Flatter.Serializer.Interfaces.Unity
{
    public interface IUnityContainerExecutor: IDisposable
    {
        IUnityContainerExecutor RegisterType<TFrom, TTo>() where TTo : TFrom;

        IUnityContainerExecutor RegisterInstance<TInterface>(TInterface instance);

        IUnityContainerExecutor RegisterFactory<TInterface>(Func<IUnityContainerExecutor, object> functor,
            IFactoryLifetimeManager lifetimeManager = null);
        IUnityContainerExecutor RegisterSingletonFactory<TInterface>(Func<IUnityContainerExecutor, object> functor);
        T Resolve<T>();
    }
}
