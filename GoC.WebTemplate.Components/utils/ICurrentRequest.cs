namespace GoC.WebTemplate.Components.Utils
{
    public interface ICurrentRequest
    {
        string QueryString { get; }
        Microsoft.AspNetCore.Http.ISession Session { get; }
    }
}