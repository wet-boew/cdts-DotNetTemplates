namespace GoC.WebTemplate.Components.Utils
{
    public interface ISessionController
    {
        bool SessionExists();
        void SetSessionValue(string key, string value);
        object GetSessionValue(string key);
    }
}