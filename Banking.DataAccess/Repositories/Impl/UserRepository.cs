using System.Collections.Generic;
using System.Linq;
using Banking.DataAccess.DataContext;
using Banking.Models;

namespace Banking.DataAccess.Repositories.Impl
{
    public class UserRepository : BaseRepository<Account>, IUserRepository
    {
        public UserRepository(DataAccsessContext context): base(context)
        {
        }

        public Account Login(string username, string password)
        {
            return _db.Accounts.SingleOrDefault(x => x.Username == username && x.Password == password);
        }

        public Account GetByUserName(string username)
        {
            return _db.Accounts.SingleOrDefault(x => x.Username == username);
        }

        public override Account Get(int id)
        {
            return _db.Accounts.SingleOrDefault(x => x.Id == id);
        }

        public override Account Save(Account entity)
        {
            throw new System.NotImplementedException();
        }

        public override Account Update(Account entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new System.NotImplementedException();
        }


        public Account Register(Account account)
        {
            _db.Accounts.Add(account);
            return account;
        }
    }
}
