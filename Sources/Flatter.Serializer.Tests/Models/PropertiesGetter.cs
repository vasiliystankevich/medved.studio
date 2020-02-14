using System.Collections.Generic;
using Flatter.Serializer.Models;

namespace Flatter.Serializer.Tests.Models
{
    public class TestProperty
    {
    }

    public class PropertiesGetterTestModel
    {
        public int Number { get; set; }
        public ETest ETest { get; set; }
        public double Double { get; set; }
        public int[] Array { get; set; }
        public TestProperty TestProperty { get; set; }
        public string String { get; set; }
        public List<string> List { get; set; }
    }
}
