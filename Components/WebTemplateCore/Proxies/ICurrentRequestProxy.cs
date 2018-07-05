using System.Web.SessionState;

namespace GoC.WebTemplate.Components.Proxies
{
    public interface ICurrentRequestProxy
    {
        string QueryString { get; }
        HttpSessionState Session { get; }
    }
}