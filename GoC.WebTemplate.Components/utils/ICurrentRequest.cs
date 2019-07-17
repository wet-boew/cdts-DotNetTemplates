namespace GoC.WebTemplate.Components.Utils
{
    public interface ICurrentRequest
    {
        string QueryString { get; }
        HttpSessionState Session { get; }
    }
}