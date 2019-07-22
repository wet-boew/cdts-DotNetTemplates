using System;

namespace GoC.WebTemplate.Components.Utils
{
    [Obsolete("Could be replaced by ICacheProvider.", false)]
    public interface ICache
    {
        void SaveToCache<T>(string key, string filename, T environments);
        T GetFromCache<T>(string key);
    }
}