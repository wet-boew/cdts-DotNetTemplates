using Microsoft.AspNetCore.Http;

namespace GoC.WebTemplate.Components.Utils
{
    public class CurrentRequest : ICurrentRequest
    {
        private HttpRequest _currentRequest;

        public CurrentRequest(HttpRequest request)
        {
            _currentRequest = request;
        }

        public string QueryString => _currentRequest.QueryString.ToString();

        public ISession Session => _currentRequest.HttpContext.Session;

    }
}