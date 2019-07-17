namespace GoC.WebTemplate.Components.Utils
{
    public class CurrentRequest : ICurrentRequest
    {
        private HttpRequestBase _currentRequest => new HttpRequestWrapper(HttpContext.Current.Request);
        public string QueryString => _currentRequest.QueryString.ToString();

        public HttpSessionState Session => HttpContext.Current.Session;

    }
}