using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Banking.Api
{
    [HubName("test")]
    public class TransactionHub : Hub
    {
        public override Task OnConnected()
        {
            var user = this.Context.User;
            return base.OnConnected();
        }

        public void Hello()
        {
            Clients.Others.hello();
        }
    }
}