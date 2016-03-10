using System.Web;

namespace Banking.Common.HttpContextWraper
{
    public class HttpContextFactory
    {
        private  static HttpContextBase _currentHttpContext;
        public static  HttpContextBase CurrentHttpContext
        {
            get { return _currentHttpContext ?? (_currentHttpContext = new HttpContextWrapper(HttpContext.Current)); }
        }

        public void SetHtpContext(HttpContextBase httpContext)
        {
            _currentHttpContext = httpContext;
        }
    }
}
