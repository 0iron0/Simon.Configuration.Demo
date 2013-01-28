using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Simon.Configuration.Demo
{
    public class ProviderElement : ConfigurationElement
    {
        #region static fields
        private static ConfigurationProperty mName;
        private static ConfigurationProperty mType;

        private static ConfigurationPropertyCollection mProperties; 
        #endregion

        #region static constructor
        static ProviderElement()
        {
            mName = new ConfigurationProperty(
                "name",
                typeof(string),
                null,
                ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
            mType = new ConfigurationProperty(
                "type",
                typeof(string),
                null,
                ConfigurationPropertyOptions.None);

            mProperties = new ConfigurationPropertyCollection();
            mProperties.Add(mName);
            mProperties.Add(mType);
        } 
        #endregion

        #region properties
        public string Name
        {
            get
            {
                return base[mName] as string;
            }
        }

        public string Provider
        {
            get
            {
                return base[mType] as string;
            }
        }

        //performance can be improved via overide this property
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return mProperties;
            }
        } 
        #endregion
    }

    public class ProviderElementCollection : ConfigurationElementCollection
    {
        #region overide
        protected override ConfigurationElement CreateNewElement()
        {
            return new ProviderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ProviderElement).Name;
        } 
        #endregion

        #region overide map type and element name

        #region comments
        //by default:
        //collection type is "ConfigurationElementCollectionType.AddRemoveClearMap"
        //element name is "add"
        //<Providers name="default">
        //  <add name="online" type="DocAve.SPMigration.Online" />
        //  <add name="offline" type="DocAve.SPMigration.Offline" />
        //</Providers> 
        #endregion

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
                
            }
        }

        protected override string ElementName
        {
            get
            {
                return "provider";
            }
        }
        #endregion

        #region indexer
        public ProviderElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as ProviderElement;
            }
        }

        public new ProviderElement this[string name]
        {
            get
            {
                return base.BaseGet(name) as ProviderElement;
            }
        } 
        #endregion
    }

    public class ProviderSection : ConfigurationSection
    {
        private static ConfigurationProperty mName;
        private static ConfigurationProperty mProviders;

        private static ConfigurationPropertyCollection mProperties;

        static ProviderSection()
        {
            mName = new ConfigurationProperty(
                "name", 
                typeof(string), 
                null, 
                ConfigurationPropertyOptions.IsRequired);
            mProviders = new ConfigurationProperty(
                "", //if it is empty, means no parent element
                typeof(ProviderElementCollection), 
                null, 
                ConfigurationPropertyOptions.IsDefaultCollection);

            mProperties = new ConfigurationPropertyCollection();
            mProperties.Add(mName);
            mProperties.Add(mProviders);
        }

        public string Name
        {
            get
            {
                return base[mName] as string;
            }
        }

        public ProviderElementCollection Providers
        {
            get
            {
                return base[mProviders] as ProviderElementCollection;
            }
        }

        //performance can be improved via overide this property
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return mProperties;
            }
        }
    }
}
