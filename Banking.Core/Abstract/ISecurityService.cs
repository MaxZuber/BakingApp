using System.Net.Http;
using Banking.Common.RequestResult;

namespace Banking.Core.Abstract
{
    public interface ISecurityService
    {
        RequestResult<string> Login(string username, string password);
        bool ValidateRequest(HttpRequestMessage request);
    }
}
