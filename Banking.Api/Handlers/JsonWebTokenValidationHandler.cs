using Banking.Common.IoC;
using Banking.Core.Abstract;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Api.Handlers
{
    public class JsonWebTokenValidationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var securityService = Ioc.Get<ISecurityService>();

            if (securityService.ValidateRequest(request))
            {
                return base.SendAsync(request, cancellationToken);
            }

            return Task.FromResult(request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorized"));
        }
    }
}