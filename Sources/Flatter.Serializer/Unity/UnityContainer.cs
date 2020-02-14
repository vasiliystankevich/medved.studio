using System;
using Flatter.Serializer.Interfaces.Unity;
using Unity;
using Unity.Lifetime;

namespace Flatter.Serializer.Unity
{
    public class UnityContainerExecutor:IUnityContainerExecutor
    {
        public UnityContainerExecutor(IUnityContainer container, IUnityContainerFunctors functors)
        {
            Container = container;
            Functors = functors;
        }

        public IUnityContainerExecutor RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            Container.RegisterType<TFrom, TTo>();
            return this;
        }

        public IUnityContainerExecutor RegisterInstance<TInterface>(TInterface instance)
        {
            Container.RegisterInstance(instance);
            return this;
        }

        public IUnityContainerExecutor RegisterFactory<TInterface>(Func<IUnityContainerExecutor, object> functor, IFactoryLifetimeManager lifetimeManager = null)
        {
            var unityFunctor = Functors.GetUnityFactoryFunctor(this, functor);
            Container.RegisterFactory<TInterface>(unityFunctor, lifetimeManager);
            return this;
        }

        public IUnityContainerExecutor RegisterSingletonFactory<TInterface>(Func<IUnityContainerExecutor, object> functor)
        {
            var unityFunctor = Functors.GetUnityFactoryFunctor(this, functor);
            Container.RegisterFactory<TInterface>(unityFunctor, new SingletonLifetimeManager());
            return this;
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        protected virtual void Dispose(bool disposed)
        {
            if (disposed) Container?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        IUnityContainer Container { get; }
        IUnityContainerFunctors Functors { get; }
    }
}
