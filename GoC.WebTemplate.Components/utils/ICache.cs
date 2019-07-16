namespace GoC.WebTemplate.Components.Utils
{
    public interface ICache
    {
        void SaveToCache<T>(string key, string filename, T environments);
        T GetFromCache<T>(string key);
    }
}