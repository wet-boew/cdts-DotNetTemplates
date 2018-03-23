
// ReSharper disable once CheckNamespace

using System.Web.SessionState;

namespace GoC.WebTemplate.Proxies
{
    public interface ICurrentRequestProxy
    {
        string QueryString { get; }
        HttpSessionState Session { get; }
    }
}