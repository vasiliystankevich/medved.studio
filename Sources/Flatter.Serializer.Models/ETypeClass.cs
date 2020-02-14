namespace Flatter.Serializer.Models
{
    public enum ETypeProperty
    {
        None = 0,
        Primitive = 1,
        Enum = 2,
        String = 4,
        Collection = 8,
        Array = 16,
        Class = 32
    }
}
