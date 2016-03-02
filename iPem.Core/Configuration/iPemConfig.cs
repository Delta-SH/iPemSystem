using System;
using System.Configuration;
using System.Xml;

namespace iPem.Core.Configuration {
    /// <summary>
    /// Represents a iPemConfig
    /// </summary>
    public partial class iPemConfig : ConfigurationSection {
        [ConfigurationProperty("sectionValue", DefaultValue = "A", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String SectionValue {
            get { return (String)this["sectionValue"]; }
            set { this["sectionValue"] = value; }
        }

        [ConfigurationProperty("iPemConfigChildSection")]
        public iPemConfigElement iPemConfigChildSection {
            get { return (iPemConfigElement)this["iPemConfigChildSection"]; }
            set { this["iPemConfigChildSection"] = value; }
        }
    }

    public partial class iPemConfigElement : ConfigurationElement {
        [ConfigurationProperty("childSectionValue1", DefaultValue = "A1", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String ChildSectionValue1 {
            get { return (String)this["childSectionValue1"]; }
            set { this["childSectionValue1"] = value; }
        }

        [ConfigurationProperty("childSectionValue2", DefaultValue = "A2", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String ChildSectionValue2 {
            get { return (String)this["childSectionValue2"]; }
            set { this["childSectionValue2"] = value; }
        }
    }
}

/* Demo
<configuration>
  <configSections>
    <sectionGroup name="iPemConfigGroup">
      <section 
        name="iPemConfigSection" 
        type="iPem.Core.Configuration.iPemConfig, iPem.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
        allowLocation="true" 
        allowDefinition="Everywhere"
      />
    </sectionGroup>
  </configSections>
  <iPemConfigGroup>
    <iPemConfigSection sectionValue="A">
      <iPemConfigChildSection childSectionValue1="A1" childSectionValue2="A2"/>
    </iPemConfigSection>
  </iPemConfigGroup>
</configuration>
*/