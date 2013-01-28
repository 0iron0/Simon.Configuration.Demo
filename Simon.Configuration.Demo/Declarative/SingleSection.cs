using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Simon.Configuration.Demo
{
    public class SingleSection : ConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired=true)]
        public string Name
        {
            get
            {
                return base["name"] as string;
            }
        }

        [ConfigurationProperty("type", IsRequired=true)]
        public string Type
        {
            get
            {
                return base["type"] as string;
            }
        }
    }
}
