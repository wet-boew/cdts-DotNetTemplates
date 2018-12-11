using System.Collections.Generic;
using CDTS_Core.WebTemplateCore;
using CDTS_Core.WebTemplateCore.JsonSerializationObjects;
using CDTS_Core.WebTemplateCore.Proxies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CDTS_Core.WebTemplate
{
    public class CDTSEnvironmentLoader
    {
        private readonly ICacheProxy _cacheProxy;

        private static readonly object EnvironmentsLockObject = new object();

        public CDTSEnvironmentLoader(ICacheProxy cacheProxy)
        {
            _cacheProxy = cacheProxy;
        }

        public IDictionary<string, ICDTSEnvironment> LoadCDTSEnvironments(IHostingEnvironment env, string filename)
        {
            IDictionary<string, ICDTSEnvironment> fromCache = _cacheProxy.GetFromCache<IDictionary<string, ICDTSEnvironment>>("GoC.Template.Environments");
            if (fromCache != null)
            {
                return fromCache;
            }
            lock (EnvironmentsLockObject)
            {
                fromCache = _cacheProxy.GetFromCache<IDictionary<string, ICDTSEnvironment>>("GoC.Template.Environments");
                if (fromCache != null)
                {
                    return fromCache;
                }
                if (filename.StartsWith("~"))
                {
                    filename = env.WebRootPath + filename.Substring(1);
                }
                fromCache = JsonSerializationHelper.DeserializeEnvironments(filename);
                _cacheProxy.SaveToCache("GoC.Template.Environments", filename, fromCache);
                return fromCache;
            }
        }
    }
}
