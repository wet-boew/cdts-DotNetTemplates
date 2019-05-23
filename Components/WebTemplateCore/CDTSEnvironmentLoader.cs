using GoC.WebTemplate.Components.JSONSerializationObjects;
using GoC.WebTemplate.Components.Proxies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;

namespace GoC.WebTemplate.Components
{
    public class CDTSEnvironmentLoader
    {
        private readonly ICacheProxy _cacheProxy;

        public CDTSEnvironmentLoader(ICacheProxy cacheProxy)
        {
            _cacheProxy = cacheProxy;
        }
        private static readonly object EnvironmentsLockObject = new object();
        /// <summary>
        /// Loads the CDTSEnvironments either from file or from the HTTPruntime.Cache 
        /// </summary>
        /// <param name="fileName">The fileName to use, we are using CDTSEnvironments.json</param>
        /// <returns>A dictionary of environments with the ICDTSEnvironment.Name being the key.</returns>
        public IDictionary<string, ICDTSEnvironment> LoadCDTSEnvironments(string fileName)
        {
            Debug.Assert(_cacheProxy != null, "CacheProxy Cannot be null");
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("The property fileName is required.");
            var environments = _cacheProxy.GetFromCache<IDictionary<string,ICDTSEnvironment>>(Constants.CACHE_KEY_ENVIRONMENTS);
            if (environments != null)
            {
                return environments;
            }

            lock (EnvironmentsLockObject)
            {
                environments = _cacheProxy.GetFromCache<IDictionary<string,ICDTSEnvironment>>(Constants.CACHE_KEY_ENVIRONMENTS);
                if (environments != null)
                {
                    return environments;
                }

                //If the path is relative we need to map it.
                if (fileName.StartsWith("~", System.StringComparison.OrdinalIgnoreCase))
                {
                    //We might want to decouple this.
                    fileName = HttpContext.Current.Server.MapPath(fileName);
                }

                //We don't catch exceptions because this file needs to exist. 
                //So we want the app to crash if it isn't.
                environments = JsonSerializationHelper.DeserializeEnvironments();
                _cacheProxy.SaveToCache(Constants.CACHE_KEY_ENVIRONMENTS,fileName, environments);
            }
            return environments;
        }
        
    }
}