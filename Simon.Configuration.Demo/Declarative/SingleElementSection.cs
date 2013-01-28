using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Simon.Configuration.Demo
{
    public class SingleElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired=true)]
        public string Name
        {
            get
            {
                return base["name"] as string;
            }
        }

        [ConfigurationProperty("value", IsRequired=true)]
        public string Value
        {
            get
            {
                return base["value"] as string;
            }
        }
    }

    public class SingleElementSection : ConfigurationSection
    {
        [ConfigurationProperty("SingleTag", IsRequired = true)]
        public SingleElement SingleTag
        {
            get
            {
                return base["SingleTag"] as SingleElement;
            }
        }
    }
}
