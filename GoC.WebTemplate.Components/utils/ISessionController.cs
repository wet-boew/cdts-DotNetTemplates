namespace GoC.WebTemplate.Components.Utils
{
    public interface ISessionController
    {
        bool SessionExists();
        void Set(string key, string value);
        object Get(string key);
    }
}