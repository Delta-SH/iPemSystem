using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace iPem.Core.Extensions {
    public class IgnoreJsonAttributesResolver : DefaultContractResolver {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization) {
            var property = base.CreateProperty(member, memberSerialization);
            property.Ignored = (member.GetCustomAttribute<JsonRedisIgnoreAttribute>(true) != null);
            return property;
        }
    }
}