using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;
using Banking.Common.Identifiers;
using Banking.Core.Abstract;
using Banking.Models;

namespace Banking.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("account")]
    public class AccountController : ApiController
    {
        private readonly ISecurityService _securityService;
        private readonly IUserService _userService;

        public AccountController(ISecurityService securityService, IUserService userService)
        {
            _securityService = securityService;
            _userService = userService;
        }

        [Route("{username}")]
        [HttpGet]
        public HttpResponseMessage Get(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _userService.IsUserAlreadyRegistered(username));
        }

        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Post(LoginViewModel loginViewModel)
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                _securityService.Login(loginViewModel.Username, loginViewModel.Password).Obj);
        }

        [HttpPost]
        [Route("signup")]
        public HttpResponseMessage Post(SignupViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var account = new Account();
                loginViewModel.UpdateEntity(account);
                var result = _userService.CreateNewUser(account);

                return result.Status == RequestStatus.Success ? Request.CreateResponse(HttpStatusCode.OK) : Request.CreateResponse(HttpStatusCode.BadRequest, result.Message);
            }

            // log error
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Model is invalid");
        }
    }
}
