namespace GoC.WebTemplate.Components.Utils.Caching
{
    public interface IFileContentCacheProvider : ICacheProvider<string>
    {
        string GetFullFilePath(string fileName, string staticFilePath);
    }
}
