using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Simon.Configuration.Demo
{
    public class MultipleElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return base["name"] as string;
            }
            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("provider", IsRequired = true)]
        public string Provider
        {
            get
            {
                return base["provider"] as string;
            }
            set
            {
                base["provider"] = value;
            }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return base["type"] as string;
            }
            set
            {
                base["type"] = value;
            }
        }
    }

    public class MultipleElementCollection : ConfigurationElementCollection
    {
        #region it may improve performance
        //private static ConfigurationPropertyCollection mProperties;

        //static MultipleElementCollection()
        //{
        //    mProperties = new ConfigurationPropertyCollection();
        //}

        //protected override ConfigurationPropertyCollection Properties
        //{
        //    get
        //    {
        //        return mProperties;
        //    }
        //} 
        #endregion

        #region abstract method
        protected override ConfigurationElement CreateNewElement()
        {
            return new MultipleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as MultipleElement).Name;
        } 
        #endregion

        #region overide parent properties
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
                return "command";
            }
        } 
        #endregion

        #region indexer
        public MultipleElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as MultipleElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                    base.BaseRemoveAt(index);
                base.BaseAdd(index, value);
            }
        }

        public new MultipleElement this[string name]
        {
            get
            {
                return base.BaseGet(name) as MultipleElement;
            }
        } 
        #endregion

        #region editable method
        public void Add(MultipleElement element)
        {
            base.BaseAdd(element);
        }

        public void Remove(string name)
        {
            base.BaseRemove(name);
        }

        public void Remove(MultipleElement element)
        {
            base.BaseRemove(GetElementKey(element));
        }

        public void RemoveAt(int index)
        {
            base.BaseRemoveAt(index);
        }

        public void Clear()
        {
            base.BaseClear();
        }

        public MultipleElement Get(string name)
        {
            return base.BaseGet(name) as MultipleElement;
        }

        public string GetElementKey(int index)
        {
            return base.BaseGetKey(index) as string;
        }

        public int IndexOf(MultipleElement element)
        {
            return base.BaseIndexOf(element);
        } 
        #endregion
    }

    public class MultipleElementSection : ConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return base["name"] as string;
            }
        }

        [ConfigurationProperty("commands", IsRequired = true, IsDefaultCollection = true)]
        public MultipleElementCollection Commands
        {
            get
            {
                return base["commands"] as MultipleElementCollection;
            }
        }
    }
}
