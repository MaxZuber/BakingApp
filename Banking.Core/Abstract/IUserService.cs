using Banking.Common.RequestResult;
using Banking.Models;

namespace Banking.Core.Abstract
{
    public interface IUserService
    {
        bool IsUserAlreadyRegistered(string username);
        RequestResult<Account> CreateNewUser(Account account);
    }
}
