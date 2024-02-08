namespace GoC.WebTemplate.Components.Utils
{
    public interface ISessionController
    {
        bool SessionExists();
        void SetSession(string key, string value);
        object GetSession(string key);
    }
}