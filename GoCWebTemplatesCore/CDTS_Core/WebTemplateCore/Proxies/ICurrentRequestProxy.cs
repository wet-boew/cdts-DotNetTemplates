using Microsoft.AspNetCore.Http;

namespace CDTS_Core.WebTemplateCore.Proxies
{
    public interface ICurrentRequestProxy
    {
        string QueryString
        {
            get;
        }

        ISession Session
        {
            get;
        }
    }
}
