using System.IO;
using System.Web;
using System.Web.SessionState;

namespace GoC.WebTemplate.Components.Framework.Tests
{
    public static class SessionCreaterTestHelper
    {
        public static void CreateSession()
        {
            CreateSession(20);
        }

#pragma warning disable xUnit1013 // Public method should be marked as test
        public static void CreateSession(int timeout)
#pragma warning restore xUnit1013 // Public method should be marked as test
        {
            //building a fake session and setting the session timeout
            var context = new HttpContext(new HttpRequest("foo", "http://www.text.com", "cue"), new HttpResponse(new StringWriter()));
            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                new HttpStaticObjectsCollection(), timeout, true,
                HttpCookieMode.AutoDetect,
                SessionStateMode.InProc, false);
            SessionStateUtility.AddHttpSessionStateToContext(context, sessionContainer);
            HttpContext.Current = context;
        }
    }
}
