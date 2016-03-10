using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.ServiceModel.Security.Tokens;
using System.Threading;
using System.Web;
using Banking.Common.HttpContextWraper;
using Banking.Common.Identifiers;
using Banking.Common.RequestResult;
using Banking.Common.Settings;
using Banking.Core.Abstract;
using Banking.DataAccess.Repositories;
using Banking.DataAccess.UnitOfWork;
using Banking.Models;

namespace Banking.Core.Impl
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public static byte[] GetBytes(string input)
        {
            var bytes = new byte[input.Length * sizeof(char)]; 
            Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length); 
            return bytes;
        }
        public RequestResult<string> Login(string username, string password)
        {
            var result = new RequestResult<string>()
            {
                Status = RequestStatus.Error
            };
            var account = _unitOfWork.UserRepository.Login(username, password);
            if (account != null)
            {
                Thread.CurrentPrincipal = CreatePrincipal(account);

                //TODO: replace with HttpContextFactory, find error reason
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = Thread.CurrentPrincipal;
                }
                result.Status = RequestStatus.Success;
                result.Obj = GetJwt(account);
            }
            return result;
        }

        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;

            if (!request.Headers.TryGetValues(Constatnts.AuthorizationHeader, out authzHeaders) || authzHeaders.Count() > 1)
            {
                // Fail if no Authorization header or more than one Authorization headers  
                // are found in the HTTP request  
                return false;
            }

            // Remove the bearer token scheme prefix and return the rest as ACS token  
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;

            return true;
        }


        public bool ValidateRequest(HttpRequestMessage request)
        {
            string token;

            if (TryRetrieveToken(request, out token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var securityKey = GetBytes(AppSettings.ClientId);

                var validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = AppSettings.Domain,
                    IssuerSigningToken = new BinarySecretSecurityToken(securityKey),
                    ValidIssuer = AppSettings.Issuer
                };

                try
                {
                    SecurityToken securityToken;
                    var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                    Thread.CurrentPrincipal = principal;

                    //TODO: replace with HttpContextFactory, find error reason
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                }
                catch (Exception exception)
                {
                    // log the exception details here 
                    //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden); 
                    return false;
                } 
            }

            return true;
        }
        private string GetJwt(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityKey = GetBytes(AppSettings.ClientId);
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetClaims(account)),
                TokenIssuerName = AppSettings.Issuer,
                AppliesToAddress = AppSettings.Domain,
                Lifetime = new Lifetime(now, now.AddHours(1)),
                SigningCredentials = new SigningCredentials(new InMemorySymmetricSecurityKey(securityKey),
                    "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                    "http://www.w3.org/2001/04/xmlenc#sha256"),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        private IEnumerable<Claim> GetClaims(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Username),
            };
            return claims;
        }
        private IPrincipal CreatePrincipal(Account account)
        {
            var claimsIdentity = new ClaimsIdentity(GetClaims(account), AuthenticationTypes.Federation, ClaimTypes.Name, ClaimTypes.Role);
            return new ClaimsPrincipal(claimsIdentity);
        }

    }
}
