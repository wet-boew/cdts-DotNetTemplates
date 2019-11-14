namespace GoC.WebTemplate.Components.Utils.Caching
{
    public interface ICacheProvider<T>
    {
        void Set(string key, T value);

        void SetWithCacheDependency(string key, string filePath, T value);

        T Get(string key);
    }
}
