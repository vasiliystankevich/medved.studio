using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Flatter.Serializer.Models;
using Flatter.Serializer.Tests.Models;
using Tests.AAAPattern.xUnit.Attributes;
using Xunit;

namespace Flatter.Serializer.Tests
{
    public class TypeClassificatorTests
    {
        [Theory]
        [MoqInlineAutoData(typeof(int), ETypeProperty.Primitive)]
        [MoqInlineAutoData(typeof(double), ETypeProperty.Primitive)]
        [MoqInlineAutoData(typeof(ETypeProperty), ETypeProperty.Enum)]
        [MoqInlineAutoData(typeof(int[]), ETypeProperty.Array)]
        [MoqInlineAutoData(typeof(string), ETypeProperty.String)]
        [MoqInlineAutoData(typeof(Collection<int>), ETypeProperty.Collection)]
        [MoqInlineAutoData(typeof(List<int>), ETypeProperty.Collection)]
        [MoqInlineAutoData(typeof(PropertiesGetterTestModel), ETypeProperty.Class)]
        public void IsAvaibleTest(Type type, ETypeProperty actual, TypeClassificator sut)
        {
            //act
            var expected = sut.GetPropertyType(type);

            //assert
            Assert.Equal(expected, actual);
        }
    }
}