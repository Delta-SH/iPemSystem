﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPem.Core.Extensions {
    /// <summary>
    /// Instructs the <see cref="JsonSerializer"/> not to serialize the public field or public read/write property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class JsonRedisIgnoreAttribute : Attribute {
    }
}
