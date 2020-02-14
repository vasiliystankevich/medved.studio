﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Flatter.Serializer.Interfaces;

namespace Flatter.Serializer.Serializers
{
    public class PrimitiveSerializer: BaseSerializer, IPrimitiveSerializer
    {
        public PrimitiveSerializer(IPrefixGenerator prefixGenerator, IPropertiesGetter propertiesGetter,
            IKeyValuePairFactory keyValuePairFactory) : base(prefixGenerator, propertiesGetter, keyValuePairFactory)
        {
        }

        protected override List<PropertyInfo> GetProperties(Type type) => PropertiesGetter.GetPrimitives(type);
    }
}
