using System.Web;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Proxies
{
    public class CurrentRequestProxy : ICurrentRequestProxy
    {
        private HttpRequestBase _currentRequest => new HttpRequestWrapper(HttpContext.Current.Request);
        public string QueryString => _currentRequest.QueryString.ToString();

    }
}