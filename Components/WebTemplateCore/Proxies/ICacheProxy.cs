namespace WebTemplateCore.Proxies
{
    public interface ICacheProxy
    {
        void SaveToCache<T>(string key, string filename, T environments);
        T GetFromCache<T>(string key);
    }
}