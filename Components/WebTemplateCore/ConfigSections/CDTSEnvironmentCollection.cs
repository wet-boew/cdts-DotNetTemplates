using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.ConfigSections
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
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            var configElement = element as CDTSEnvironmentElement;
            if (configElement == null)
            {
                throw new ArgumentException($"{nameof(element)} must be of type CDTSEnviromentElement");
            }
            return configElement.Key;
        }


        public CDTSEnvironmentElement this[int index] => BaseGet(index) as CDTSEnvironmentElement;

        public new CDTSEnvironmentElement this[string key]
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