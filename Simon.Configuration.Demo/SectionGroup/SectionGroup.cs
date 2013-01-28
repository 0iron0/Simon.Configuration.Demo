using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Simon.Configuration.Demo
{
    public class SectionGroup : ConfigurationSectionGroup
    {
        [ConfigurationProperty("first")]
        public SingleElementSection First
        {
            get
            {
                return base.Sections["first"] as SingleElementSection;
            }
        }

        [ConfigurationProperty("second")]
        public SingleSection Second
        {
            get
            {
                return base.Sections["second"] as SingleSection;
            }
        }
        
    }
}
