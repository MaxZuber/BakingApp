using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Banking.Api.Controllers
{
    [RoutePrefix("AccountInfo")]
    public class AccountInfoController : ApiController
    {
        [Route("Balance")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, 250);
        }
    }
}