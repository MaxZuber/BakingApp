using Banking.Models;

namespace Banking.DataAccess.Repositories
{
    public interface IUserRepository : IRepository<Account>
    {
        Account Login(string username, string password);
        Account GetByUserName(string username);
        Account Register(Account account);
    }
}
