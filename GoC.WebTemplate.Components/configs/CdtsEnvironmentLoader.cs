using System;
using System.Collections.Generic;
using System.Diagnostics;
using GoC.WebTemplate.Components.Utils;

namespace GoC.WebTemplate.Components.Configs
{
    [Obsolete("Could be replaced by CdtsEnvironmentCache.", false)]
    public class CdtsEnvironmentLoader
    {
        private readonly ICache _cacheProxy;

        public CdtsEnvironmentLoader(ICache cacheProxy)
        {
            _cacheProxy = cacheProxy;
        }
        private static readonly object EnvironmentsLockObject = new object();
        /// <summary>
        /// Loads the CDTSEnvironments either from file or from the HTTPruntime.Cache 
        /// </summary
        /// <returns>A dictionary of environments with the ICDTSEnvironment.Name being the key.</returns>
        public IDictionary<string, ICdtsEnvironment> LoadCDTSEnvironments()
        {
            Debug.Assert(_cacheProxy != null, "CacheProxy Cannot be null");
            var environments = _cacheProxy.GetFromCache<IDictionary<string,ICdtsEnvironment>>(Constants.CACHE_KEY_ENVIRONMENTS);
            if (environments != null)
            {
                return environments;
            }

            lock (EnvironmentsLockObject)
            {
                var filename = "~/configs/CdtsEnvironments.json";
                environments = _cacheProxy.GetFromCache<IDictionary<string,ICdtsEnvironment>>(Constants.CACHE_KEY_ENVIRONMENTS);
                if (environments != null)
                {
                    return environments;
                }

                // Todo: Comment out until moved to appropreate project
                ////If the path is relative we need to map it.
                //if (filename.StartsWith("~"))
                //{
                //    //We might want to decouple this.
                //    filename = HttpContext.Current.Server.MapPath(filename);
                //}

                //We don't catch exceptions because this file needs to exist. 
                //So we want the app to crash if it isn't.
                environments = JsonSerializationHelper.DeserializeEnvironments();
                _cacheProxy.SaveToCache(Constants.CACHE_KEY_ENVIRONMENTS,filename, environments);
            }
            return environments;
        }
        
    }
}