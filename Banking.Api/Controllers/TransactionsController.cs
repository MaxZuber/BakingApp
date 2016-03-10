using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Banking.Core.Abstract;
using Banking.Models;

namespace Banking.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("transactions")]
    public class TransactionsController : ApiController
    {

        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Route("history")]
        public HttpResponseMessage Get(int page = 1, int itemsPerPage = 10)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //[Route("{id}")]
        //public HttpResponseMessage Get(int id)
        //{
            
        //}
        [Route("transfermoney")]
        [HttpPost]
        public HttpResponseMessage Post(TransferMoneyViewModel transferMoneyViewModel)
        {
            _transactionService.TransferMoney(transferMoneyViewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}