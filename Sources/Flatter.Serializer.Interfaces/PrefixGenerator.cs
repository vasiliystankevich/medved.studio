using System;
using System.Collections.Generic;
using System.Text;

namespace Flatter.Serializer.Interfaces
{
    public interface IPrefixGenerator
    {
        string Generate(string path, string propertyName);
    }
}
