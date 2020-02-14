using Flatter.Serializer.Interfaces;

namespace Flatter.Serializer
{
    public class PrefixGenerator: IPrefixGenerator
    {
        public string Generate(string path, string propertyName)
        {
            return string.IsNullOrEmpty(path) ? propertyName : $"{path}.{propertyName}";
        }
    }
}
