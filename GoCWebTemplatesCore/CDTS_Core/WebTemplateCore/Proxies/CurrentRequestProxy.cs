using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace CDTS_Core.WebTemplateCore.Proxies
{
    public class CurrentRequestProxy : ICurrentRequestProxy
    {
        public CurrentRequestProxy(HttpRequest request)
        {
            _currentRequest = request;
        }
        private HttpRequest _currentRequest;

        public string QueryString => _currentRequest.QueryString.ToString();

        public ISession Session => _currentRequest.HttpContext.Session;
    }

}
