using System;
using AutoFixture.Xunit2;
using Flatter.Serializer.Interfaces.Unity;
using Flatter.Serializer.Tests.Models;
using Flatter.Serializer.Unity;
using FluentAssertions;
using Moq;
using Tests.AAAPattern.xUnit.Attributes;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Xunit;

namespace Flatter.Serializer.Tests
{
    public class UnityContainerExecutorTests
    {
        [Theory]
        [MoqAutoData]
        public void RegisterTypeTest([Frozen]Mock<IUnityContainer> container, UnityContainerExecutor sut)
        {
            //act
            var expected = sut.RegisterType<IResolveInterface, ResolveClass>();

            //assert
            container.Verify(service => service.RegisterType(typeof(IResolveInterface), typeof(ResolveClass),
                null, null, It.IsAny<InjectionMember[]>()));
            expected.Should().BeEquivalentTo(sut);
        }

        [Theory]
        [MoqAutoData]
        public void RegisterInstance(IResolveInterface actual, [Frozen]Mock<IUnityContainer> container, UnityContainerExecutor sut)
        {
            //act
            var expected = sut.RegisterInstance(actual);

            //assert
            container.Verify(
                service => service.RegisterInstance(typeof(IResolveInterface), null, actual,
                    It.IsNotNull<ContainerControlledLifetimeManager>()), Times.AtLeastOnce);
            expected.Should().BeEquivalentTo(sut);
        }

        [Theory]
        [MoqAutoData]
        public void RegisterFactoryTest(Func<IUnityContainerExecutor, object> functor,
            Func<IUnityContainer, object> unityFunctor, [Frozen] Mock<IUnityContainer> container,
            [Frozen] Mock<IUnityContainerFunctors> functors, UnityContainerExecutor sut)
        {
            //arrange
            functors.Setup(service => service.GetUnityFactoryFunctor(sut, functor)).Returns(unityFunctor);

            //act
            var expected = sut.RegisterFactory<IResolveInterface>(functor);

            //assert
            functors.Verify(service => service.GetUnityFactoryFunctor(sut, functor), Times.AtLeastOnce);
            container.Verify(
                service => service.RegisterFactory(typeof(IResolveInterface), It.IsAny<string>(),
                    It.IsNotNull<Func<IUnityContainer, Type, string, object>>(),
                    It.IsAny<IFactoryLifetimeManager>()), Times.AtLeastOnce);
            expected.Should().BeEquivalentTo(sut);
        }

        [Theory]
        [MoqAutoData]
        public void RegisterSingletonFactory(Func<IUnityContainerExecutor, object> functor,
            Func<IUnityContainer, object> unityFunctor, [Frozen] Mock<IUnityContainer> container,
            [Frozen] Mock<IUnityContainerFunctors> functors, UnityContainerExecutor sut)
        {
            //arrange
            functors.Setup(service => service.GetUnityFactoryFunctor(sut, functor)).Returns(unityFunctor);

            //act
            var expected = sut.RegisterSingletonFactory<IResolveInterface>(functor);

            //assert
            functors.Verify(service => service.GetUnityFactoryFunctor(sut, functor), Times.AtLeastOnce);
            container.Verify(
                service => service.RegisterFactory(typeof(IResolveInterface), It.IsAny<string>(),
                    It.IsNotNull<Func<IUnityContainer, Type, string, object>>(),
                    It.IsNotNull<SingletonLifetimeManager>()), Times.AtLeastOnce);
            expected.Should().BeEquivalentTo(sut);
        }

        [Theory]
        [MoqAutoData]
        public void Resolve(IResolveInterface actual, [Frozen]Mock<IUnityContainer> container, UnityContainerExecutor sut)
        {
            //arrange
            container.Setup(service => service.Resolve(typeof(IResolveInterface), null)).Returns(actual);

            //act
            var expected = sut.Resolve<IResolveInterface>();

            //assert
            container.Verify(service => service.Resolve(typeof(IResolveInterface), null), Times.AtLeastOnce);
            expected.Should().BeEquivalentTo(actual);
        }
    }
}
