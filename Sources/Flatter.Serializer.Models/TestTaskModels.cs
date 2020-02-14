using System.Collections.Generic;

namespace Flatter.Serializer.Models
{
    public enum ETest
    {
        A=1, B=2
    }

    public class Snapshot
    {
        public int[] Array { get; set; }
        public List<string> List { get; set; }
        public string String { get; set; }
        public double Double { get; set; }
        public ETest ETest { get; set; }
        public int Number { get; set; }
        public int Md { get; set; }
        public int Pressure { get; set; }
        public Density Density { get; set; }
        public MassFractions MassFractions { get; set; }
    }
    public class Density
    {
        public int Liquid { get; set; }
        public int Gas { get; set; }
    }
    public class MassFractions
    {
        public Component[] Liquid { get; set; }
        public List<Component> Gas { get; set; }
    }
    public class Component
    {
        public string Key { get; set; }
        public double Value { get; set; }
    }
}
