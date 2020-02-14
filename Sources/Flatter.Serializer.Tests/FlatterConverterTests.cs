using Flatter.Serializer.Interfaces;
using Flatter.Serializer.Models;
using Project.Kernel;
using Tests.AAAPattern.xUnit.Attributes;
using Unity;
using Xunit;

namespace Flatter.Serializer.Tests
{
    public class FlatterConverterTests
    {
        [Theory]
        [MoqInlineAutoData(26)]
        public void SerializationAvaibleTest(int actualCountElements, Snapshot data)
        {
            //arrange
            var container = UnityConfig.GetConfiguredContainer();
            BaseTypeFabric.RegisterTypes<TypeFabric>(container);
            var sut = container.Resolve<IFlatterConverter>();

            //act
            var result = sut.SerializeObject(data);

            //assert 
            Assert.NotNull(result);
            Assert.IsType<FlatterResult>(result);
            Assert.NotNull(result.Keys);
            Assert.NotNull(result.Values);
            Assert.Equal(actualCountElements, result.Keys.Count);
            Assert.Equal(actualCountElements, result.Values.Count);
        }
    }
}
