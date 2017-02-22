using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace GOC.WebTemplate.ConfigSections
{
    [ConfigurationCollection(typeof(CDTSEnvironmentElement), AddItemName = "cdtsEnvironment")]
    public class CDTSEnvironmentCollection : ConfigurationElementCollection, IEnumerable<CDTSEnvironmentElement>, IComparer<CustomStringComparer>
    {
        public int Compare(CustomStringComparer aa, CustomStringComparer bb)
        {
            return 1;
        }


        protected override ConfigurationElement CreateNewElement()
        {
            return new CDTSEnvironmentElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var l_configElement = element as CDTSEnvironmentElement;
            if (l_configElement != null)
                return l_configElement.Key;
            else
                return null;
        }


        public CDTSEnvironmentElement this[int index]
        {
            get
            {
                return BaseGet(index) as CDTSEnvironmentElement;
            }
        }

        public CDTSEnvironmentElement this[string key]
        {
            get
            {

                foreach (CDTSEnvironmentElement cdtsEnvironmentElement in this)
                {
                    if (string.Compare(cdtsEnvironmentElement.Key, key, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        return cdtsEnvironmentElement;
                    }
                    
                }
                return null;
            }
        }

        IEnumerator<CDTSEnvironmentElement> IEnumerable<CDTSEnvironmentElement>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                .GetEnumerator();
        }
    }
}