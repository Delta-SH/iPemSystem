using System;
using System.Configuration;

namespace iPem.Core.Configuration {
    /// <summary>
    /// Represents a RedisConfig
    /// </summary>
    public partial class RedisConfig : ConfigurationSection {
        public static RedisConfig GetConfig() {
            var section = ConfigurationManager.GetSection("RedisConfigGroup/RedisConfigSection") as RedisConfig;
            return section;
        }

        public static RedisConfig GetConfig(string sectionName) {
            var section = ConfigurationManager.GetSection(sectionName) as RedisConfig;
            if(section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }

        [ConfigurationProperty("WriteServerList", DefaultValue = "127.0.0.1:6379", IsRequired = true)]
        [StringValidator(MinLength = 1, MaxLength = 100)]
        public String WriteServerList {
            get { return (String)this["WriteServerList"]; }
            set { this["WriteServerList"] = value; }
        }

        [ConfigurationProperty("ReadServerList", DefaultValue = "127.0.0.1:6379", IsRequired = true)]
        [StringValidator(MinLength = 1, MaxLength = 100)]
        public String ReadServerList {
            get { return (String)this["ReadServerList"]; }
            set { this["ReadServerList"] = value; }
        }

        [ConfigurationProperty("MaxWritePoolSize", DefaultValue = 50, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 10000, ExcludeRange = false)]
        public Int32 MaxWritePoolSize {
            get { return (Int32)this["MaxWritePoolSize"]; }
            set { this["MaxWritePoolSize"] = value; }
        }

        [ConfigurationProperty("MaxReadPoolSize", DefaultValue = 50, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 10000, ExcludeRange = false)]
        public Int32 MaxReadPoolSize {
            get { return (Int32)this["MaxReadPoolSize"]; }
            set { this["MaxReadPoolSize"] = value; }
        }

        [ConfigurationProperty("LocalCacheTime", DefaultValue = 60L, IsRequired = false)]
        [LongValidator(MinValue = 1L, MaxValue = 2592000L, ExcludeRange = false)]
        public Int64 LocalCacheTime {
            get { return (Int64)this["LocalCacheTime"]; }
            set { this["LocalCacheTime"] = value; }
        }

        [ConfigurationProperty("AutoStart", DefaultValue = true, IsRequired = false)]
        public Boolean AutoStart {
            get { return (Boolean)this["AutoStart"]; }
            set { this["AutoStart"] = value; }
        }
    }
}
